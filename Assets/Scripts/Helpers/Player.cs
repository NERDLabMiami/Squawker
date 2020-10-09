using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public TextAsset potentialMessages;
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
	private int character_id;
	public Tracker tracker;
	public GameObject notification;
	private Sprite avatar;

	public bool finishedConversation = false;

	void Start() {
		if (potentialMessages)
		{
			json = JSON.Parse(potentialMessages.ToString());
			Debug.Log("Loading chat...");
			character = PlayerPrefs.GetString("chatting_with");
			character_id = PlayerPrefs.GetInt("character id", 0);
			avatar = Resources.Load<Sprite>("Characters/" + character);

			string[] chat_path_history = PlayerPrefsX.GetStringArray(character);
			string[] chat_path_response_history = PlayerPrefsX.GetStringArray(character + "_responses");
			int responseCounter = 0;

			for (int i = 0; i < chat_path_history.Length; i++)
			{
				SendMessageToPlayer(character, chat_path_history[i], false);
				if(i < chat_path_response_history.Length)
				{
					chatLog.addResponseToChatLog(chat_path_response_history[i]);
					responseCounter++;
				}

			}

			if (responseCounter < chat_path_response_history.Length)
			{
				Debug.LogError("still remaining responses in archive");
			}

			if (chat_path_history.Length == 0)
			{
				SendMessageToPlayer(character, "intro", true);

			}


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


	public void SendMessageToPlayer(string character, string _passage, bool archive)
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

		chatLog.addMessage(message, avatar);
		sender = character;
		passage = _passage;
		
		if(archive) { 
			List<string> chat_path_history = PlayerPrefsX.GetStringArray(character).ToList();
			chat_path_history.Add(passage);
			Debug.Log("character: " + character + " passage:" + _passage);
			PlayerPrefsX.SetStringArray(character, chat_path_history.ToArray());
			ToggleResponseOptions();
			if(passage.Contains("finished"))
			{
				PlayerPrefs.SetInt("intervention index", 1);
				//show notification
				Notification notice = Instantiate(notification, chatLog.transform.parent.parent.parent).GetComponent<Notification>();
				notice.SetNotice("Amelia has sent you a message", 5);
			}
		}
		else {
			Debug.Log("not archiving...");
			Debug.Log("passage: " + passage);
			if (passage.Contains("finished"))
			{
				
				Debug.Log("Deactivating Responses");
				chatLog.respondButton.SetActive(false);
				chatLog.responseContainer.SetActive(false);
				chatLog.waitingForResponse = false;

			}
			else
			{
				ToggleResponseOptions();

			}


		}

		if (tracker)
		{
			tracker.Track();

		}
	}


	private void ToggleResponseOptions()
	{
		chatLog.waitingForResponse = true;
		chatLog.responsesPopulated = false;

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

	public void chatWithCharacter(string _character, int index)
	{
		PlayerPrefs.SetString("chatting_with", _character);
		PlayerPrefs.SetInt("character id", index);
		StartCoroutine(loadLevel(3));
	}

		
}
