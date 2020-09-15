using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public TextAsset potentialMessages;
	public bool reset = false;
	//	public List<Message> inbox;
	public Inbox chatLog;
	public Profile profile;
	public GameObject loadingScreen;
	public JSONNode json;
	private List<string> messageList;
	private string character;
	private int previousMessageCount = 0;
	private string sender;
	private string passage;
	private Message message;
	// Use this for initialization



	void Start() {
		if (reset) {
			ResetChats();
		}
		json = JSON.Parse(potentialMessages.ToString());
		//inbox = new List<Message>();
		//			refreshInbox();

		//TODO: Update based on feed comment/DM
		if (SceneManager.GetActiveScene().name == "Chat")
		{
			//set conversation thread

			character = PlayerPrefs.GetString("chatting_with");

			SendMessageToPlayer(character, "intro");
		}
	}
	
	private void ResetChats()
	{
		if(reset)
		{
			PlayerPrefs.DeleteKey("chats");
		}
	}
	public string getCharacter()
	{
		return character;
	}

	public void ClearResponseOptions()
	{
		//REMOVE PREVIOUS RESPONSE OPTIONS
		foreach (Transform child in chatLog.responseOptions.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

	public void PopulateResponses() { 

		JSONNode responses = json[sender][passage]["responses"];
		for (int i = 0; i < responses.Count; i++)
		{
			Response r = new Response(responses[i]["path"], responses[i]["response"], i, responses[i]["belief_id"], character);
			message.responses.Add(r);
			chatLog.addResponse(r);
		}
		chatLog.responseOptions.SetActive(false);

	}

	public void SendMessageToPlayer(string character, string _passage)
	{
		message = new Message();
		message.sender = character;
		message.passage = _passage;
		message.belief = json[message.sender][message.passage]["belief_id"];
		message.body = json[message.sender][message.passage]["message"];

		//store conversation in player prefs. Example anxiety_intro = "Hey, you've been missing class a lot. You alright?"
		PlayerPrefs.SetString(character + "_" + passage, message.body);
		
		chatLog.addMessage(message);
		sender = character;
		passage = _passage;
		chatLog.waitingForResponse = true;
		chatLog.responsesPopulated = false;
	}

	/*
		public bool hooked() {
			int h = PlayerPrefs.GetInt ("hooked", 0);
			if (h == 0) {
				return false;
			}
			else {
				return true;
			}
		}
		public void unhook() {
			PlayerPrefs.SetInt ("hooked", 0);
		}

		public void hook() {
			PlayerPrefs.SetInt ("hooked", 1);
		}

		public void setGenderPreference(string gender) {
			PlayerPrefs.SetString ("gender preference", gender);
			populateMatches ();

		}
		public string getFinalStory(string character, string story) {
			if (json[character] != null) {
				//return json[character]["epilogue"][story];
				return json[character]["epilogue"][story]["story"];
			}
			else {
				return "no story available";
			}
		}

		public int getEpilogueType(string character, string story) {
			if (json[character] != null) {
				return json[character]["epilogue"][story]["ending"].AsInt;
			}
			else {
				return 0;
			}
		}
		public int matches(string characterPath) {
			int responseTime = 0;
			if (json[characterPath] != null) {
				responseTime = json [characterPath] ["requirements"] ["love"].AsInt;
				int matchingBonus = 3;
	//			responseTime -= attractiveness;
				responseTime-=matchingBonus;
				}

				if (responseTime < 0) {
					responseTime = 1;
					Debug.Log ("Immediate attraction");
				}
			Debug.Log ("Match will respond in " + responseTime + " days");
			return responseTime;
		}

		public int matches2(string characterPath) {
			//Get character's attractiveness, between 0 and 100 to set as the initial response time

			if (json[characterPath] != null) {

				int responseTime = json [characterPath] ["requirements"] ["love"].AsInt;

				//Get the difference between the two characters attractiveness. NPC with 10, player with 5, response time is 5.
				//NPC with 5 player with attractiveness of 10, response time is -5
				//typical love should be near 5
		//		responseTime -= attractiveness;

				responseTime += json[characterPath]["requirements"]["accessories"]["headwear"].AsInt;

				//If the response time is negative, we just set it to 0 so the NPC responds instantly

				if (responseTime < 0) {
					responseTime = 1;
					Debug.Log ("So attractive, response is almost immediate");
				}
				Debug.Log ("Match will respond in " + responseTime + " days");
				return responseTime;
			} else {
				Debug.Log("Can't Find Character Path : " + characterPath);
				return 9999;
			}
		}
		*/

	/*
public void refreshInbox() {
	previousMessageCount = inbox.Count;
	inbox.Clear();
	for (int i = 0; i < messageList.Count; i++) {
		string[] messageParts = StringArrayFunctions.getMessage(messageList[i]);
		Debug.Log("Message Parts: " + messageList[i]);
		if (!messageParts[0].Equals("ignore")) {
			if (int.Parse(messageParts[2]) <= 0) {
					//new message, add to list
					Message message = new Message();
					message.index = i;
					message.path = messageList[i];
					message.sender = messageParts[0];
					message.passage = messageParts[1];
					message.belief = json[message.sender][message.passage]["belief_id"];						
					message.body = json[message.sender][message.passage]["message"];
					JSONNode responses = json[message.sender][message.passage]["responses"];
					for (int j = 0; j < responses.Count; j++) {
					Response r = new Response(responses[j]["path"], responses[j]["response"], responses[j]["time"].AsInt, i, responses[j]["belief_id"]);
						message.responses.Add(r);
					}

					inbox.Add(message);

			}
		}
	}

}



private void saveMessageList() {
	Prefs.PlayerPrefsX.SetStringArray("messages", messageList.ToArray());
}
*/
	public void addMessage(string path) {
		messageList.Add(path);
		//		saveMessageList();
	}

	public void removeMessage(int index) {
		messageList.RemoveAt(index);
		//		saveMessageList();
	}

	public void removeAllMessages() {
		messageList.Clear();
		//		saveMessageList();
	}
	/*
	private void saveProgress(int actions, int days) {
		PlayerPrefs.SetInt("actions left", actions);
		PlayerPrefs.SetInt("days left", days);
	}
    private void newDay()
    {

        for (int i = 0; i < messageList.Count; i++)
        {
            //iterate through inbox, reduce wait time for each message
            string[] message = StringArrayFunctions.getMessage(messageList[i]);
            int currentDuration = int.Parse(message[2]);

            if (currentDuration > 0)
            {
                //TODO: Check tan requirements here and if not met, add 1?
                //int requiredTan = json[message[0]][message[1]]["thresholds"]["tan"].AsInt;
                int requiredTan = json[message[0]]["requirements"]["tan"].AsInt;
                //TODO: Look at parts of message
                messageList[i] = message[0] + "/" + message[1] + "/" + currentDuration.ToString();
            }
            saveMessageList();
        }

    }
	public void refresh() {
	}
	public bool takeAction(bool takeTolls) {
			newDay();
        //REFRESHING INBOX HERE
        //refresh();
        return true;
    }


	public void populateMatches() {
		List<string> list = new List<string>();
		List<string>fizzle = new List<string>();
		string gender = PlayerPrefs.GetString ("gender preference", "both");
		string gender_list = "";
		switch (gender) {
		case "men":
			gender_list = "male_characters";
			break;
		case "women":
			gender_list = "female_characters";
			break;
		case "both":
			gender_list = "both_characters";
			break;
		default:
			Debug.Log ("No Gender Preference Assigned");
			break;
		}
		for (int i = 0; i < json["fizzles"].Count; i++) {
			fizzle.Add(json["fizzles"][i]);
		}
		for (int i = 0; i < json[gender_list].Count; i++) {
			list.Add(json[gender_list][i]);
		}

		Prefs.PlayerPrefsX.SetStringArray("fizzles", fizzle.ToArray());
		Prefs.PlayerPrefsX.SetStringArray(gender, list.ToArray());
		Debug.Log("Saved " + list.Count + " characters");

	}

	public void gameOver() {
		PlayerPrefs.DeleteAll();
	}

	public void resetStats() {
//		PlayerPrefs.DeleteKey ("messages");
		PlayerPrefs.DeleteAll();
		removeAllMessages();

	}
	
	private void populateStats() {
		
	}

	public void updateProfile() {
		if (profile) {
			profile.messageCount.text = inbox.Count.ToString();

			if(inbox.Count <= 0) {
				profile.messageCount.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
//				previewInbox.emptyMailboxMessage.SetActive(true);
			}
			else {
				profile.messageCount.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
//				previewInbox.emptyMailboxMessage.SetActive(false);
			}
			
		}
	}

	public string fetchMaleName() {
		int numNames = json["names"]["men"].Count;
		return json["names"]["men"][Random.Range(0,numNames)];
	}

	public string fetchMotto() {
		int numMottos = json["mottos"].Count;
		return json["mottos"][Random.Range (0, numMottos)];
	}
    */
	//	StartCoroutine (loadHome ());

	public void loadSceneNumber(int level) {
		//		loadingScreen.SetActive(true);
		StartCoroutine(loadLevel(level));
	}

	IEnumerator loadLevel(int level) {
		AsyncOperation async = SceneManager.LoadSceneAsync(level);
		while (!async.isDone) {
			Debug.Log("ASYNC: " + async.progress);
			yield return async;
		}
		//	loadingScreen.SetActive (false);
		Debug.Log("Loading complete");
	}

	public void chatWithCharacter(string _character)
	{
		PlayerPrefs.SetString("chatting_with", _character);
		StartCoroutine(loadLevel(2));
	}

    /*
	public void returnHome(){
		loadSceneNumber(1);
	}
    */
		
}
