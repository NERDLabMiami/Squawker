using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class InboxDebug : MonoBehaviour {

	public TextAsset characters;
	public GameObject messageTemplate;
	private JSONNode json;
	private List<string> inbox;
//	private string[] messages;
	// Use this for initialization
	void Start () {
//		Prefs.PlayerPrefsX.SetStringArray("messages", null);
		json = JSON.Parse(characters.ToString());
		string[] messages = Prefs.PlayerPrefsX.GetStringArray("messages", null, 0);
		if (messages.Length == 0) {
			Debug.Log("0 Messages");
		}
		else {
			Debug.Log(messages.Length + " messages");
		}

		inbox = messages.OfType<string>().ToList();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void newMessage() {
		inbox.Add("bob/intro/4");
		Prefs.PlayerPrefsX.SetStringArray("messages", inbox.ToArray());
	}

	public void advanceDay() {
		if (inbox.Count > 0) {
			Debug.Log(inbox.Count + " messages, moving forward");
			for(int i = 0; i < inbox.Count; i++) {
				//iterate through inbox, reduce wait time for each message
			//	string[] message = inbox[i].Split (new string[] {"/"}, System.StringSplitOptions.None);
				string[] message = getMessage (inbox[i]);
				int duration = int.Parse(message[2]);
				if(duration > 0) {
					duration--;
				}
				inbox[i] = message[0] + "/" + message[1] + "/" + duration.ToString();
				Debug.Log("Message Now : " + inbox[i]);
			}
	
			Prefs.PlayerPrefsX.SetStringArray("messages", inbox.ToArray());

		}

	}

	private string[] getMessage(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}

	public void newIncomingMessage(string sender, string text, string path) {
		GameObject message = Instantiate(messageTemplate);
		message.transform.SetParent(this.gameObject.transform, false);
		message.GetComponent<IncomingMessage>().sender.text = sender;
		message.GetComponent<IncomingMessage>().message.text = text;
		message.GetComponent<IncomingMessage>().path = path;

	}
	public void clearInbox() {
		inbox.Clear();
		Prefs.PlayerPrefsX.SetStringArray("messages", inbox.ToArray());

	}

	public void refresh() {
		for (int i = 0; i < inbox.Count; i++) {
			string[] message = getMessage (inbox[i]);
			if (int.Parse(message[2]) <= 0) {
				Debug.Log("Message Available");
				string body = json[message[0]][message[1]]["subject"];
				newIncomingMessage(message[0], body, inbox[i]);
			}
		}

	}

}
