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
	public Tracker tracker;


	void Start() {
		if (reset) {
			ResetChats();
		}
		if (potentialMessages)
		{
			json = JSON.Parse(potentialMessages.ToString());
			//inbox = new List<Message>();
			//			refreshInbox();
		}
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
	private void FindTracker()
	{
		if (!tracker)
		{
			tracker = FindObjectOfType<Tracker>();
		}
	}


	public void SendMessageToPlayer(string character, string _passage)
	{
		FindTracker();
		message = new Message();
		message.sender = character;
		message.passage = _passage;
		message.belief = json[message.sender][message.passage]["belief_id"];
		if (tracker)
		{
			tracker.belief_id = message.belief;
			tracker.path = _passage;
			tracker.character = character;
		}
			message.body = json[message.sender][message.passage]["message"];

		//TODO: save belief

		//store conversation in player prefs. Example anxiety_intro = "Hey, you've been missing class a lot. You alright?"
		PlayerPrefs.SetString(character + "_" + passage, message.body);
		
		chatLog.addMessage(message);
		sender = character;
		passage = _passage;
		chatLog.waitingForResponse = true;
		chatLog.responsesPopulated = false;
		if(tracker)
		{
			tracker.Track();

		}
	}


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
		StartCoroutine(loadLevel(3));
	}

		
}
