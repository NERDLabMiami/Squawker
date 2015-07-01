using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class Inbox : MonoBehaviour {
	public TextAsset characters;
	public IncomingMessage messageTemplate;
	public GameObject messageContainer;
	private JSONNode json;
	private List<string> inbox;


	// Use this for initialization
	void Start () {
		json = JSON.Parse(characters.ToString());
		string[] messages = Prefs.PlayerPrefsX.GetStringArray("messages", null, 0);
		if (messages.Length == 0) {
			Debug.Log("0 Messages");
		}
		else {
			Debug.Log(messages.Length + " messages");
		}
		
		inbox = messages.OfType<string>().ToList();
		show ();
	}

	public int getCount() {
		return inbox.Count;
	}

	public void reduceDuration(int index, int amount) {
		string[] message = getMessage (inbox[index]);
		int currentDuration = int.Parse(message[2]);
		if(currentDuration > 0) {
			currentDuration-=amount;
		}
		inbox[index] = message[0] + "/" + message[1] + "/" + currentDuration.ToString();
	}

	private string[] getMessage(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}
	
	public void save() {
		Prefs.PlayerPrefsX.SetStringArray("messages", inbox.ToArray());
	}

	public void clear() {
		inbox.Clear();
		save();
	}

	public void removeMessage(int index) {
		inbox.RemoveAt(index);
		save ();
	}

	public void newMessage(string path) {	
		inbox.Add(path);
		save ();
	}

	public void refresh() {
		foreach (Transform child in messageContainer.transform)
		{
			Destroy(child.gameObject);
		}
		
		show ();
	}
	
	public void show() {
			for (int i = 0; i < inbox.Count; i++) {
				string[] message = getMessage (inbox[i]);
				if (int.Parse(message[2]) <= 0) {
					Debug.Log("Message Available");
					string body = json[message[0]][message[1]]["subject"];
					newIncomingMessage(message[0], body, inbox[i], i);
				}
			}
	}

	public void newIncomingMessage(string sender, string text, string path, int idx) {
		GameObject message = Instantiate(messageTemplate.gameObject);

		message.transform.SetParent(messageContainer.transform, false);
		message.GetComponent<IncomingMessage>().sender.text = sender;
		message.GetComponent<IncomingMessage>().message.text = text;
		message.GetComponent<IncomingMessage>().path = path;
		message.GetComponent<IncomingMessage>().index = idx;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
