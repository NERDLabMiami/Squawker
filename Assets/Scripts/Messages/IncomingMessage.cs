using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using SimpleJSON;
using System;
using System.Collections.Generic;

public class IncomingMessage : MonoBehaviour {
	public string character;
	public Text subject;
	public ViewMessage expandedMessageTemplate;
	public Character avatar;
	public Image overrideImage;
	public AudioClip click;

	private Message message;
	public PlayerBehavior player;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();

	}

	public void setMessage(Message msg) {
		message = msg;
		character = message.sender;
		message.belief = msg.belief;
		avatar.gameObject.SetActive(false);
		overrideImage.enabled = true;
		switch(character) {
			case "custom":
				overrideImage.sprite = Resources.Load<Sprite>("Folder/Path");
				break;
		default:
				overrideImage.enabled = false;
				avatar.gameObject.SetActive(true);
				break;
		}
		message.alias = PlayerPrefs.GetString(character);
		avatar.assign(character);
		subject.text = message.subject;
	}

	public void show() {

		Camera.main.GetComponent<AudioSource> ().PlayOneShot (click);
		GameObject msg = Instantiate(expandedMessageTemplate.gameObject);
		msg.GetComponent<ViewMessage>().body.text = message.body;
		msg.GetComponent<ViewMessage>().profilePic.enabled = true;
		msg.GetComponent<ViewMessage>().character.gameObject.SetActive(false);

		Debug.Log("SENDER: " + message.sender);
			int eventId = 0;
			string subtype = "";
			string characterSendingMessage = "";
			switch (message.sender) {
		case "custom":
			characterSendingMessage = "Jane Doe";
			eventId = 3;
			subtype = StringArrayFunctions.getMessage(message.path)[1];
			msg.GetComponent<ViewMessage>().alias.text = "Alias Name";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("Custom/Path");

			break;
		default:
			eventId = 1;
			subtype = "DLG";
			characterSendingMessage = message.sender;
			msg.GetComponent<ViewMessage>().character.assign(message.sender);
			msg.GetComponent<ViewMessage>().character.assign(character);
			msg.GetComponent<ViewMessage>().alias.text = msg.GetComponent<ViewMessage>().character.name;
			msg.GetComponent<ViewMessage>().profilePic.enabled = false;
			msg.GetComponent<ViewMessage>().character.gameObject.SetActive(true);

			break;

		}
        Debug.Log("Submitting Event");
        //SEND TRACK EVENT FOR PLAYER SEEING MESSAGE
        player.trackEvent(eventId, subtype,message.belief,characterSendingMessage);
		msg.transform.SetParent(this.gameObject.transform.parent.parent.parent.parent, false);
		this.transform.parent.gameObject.SetActive(false);

		for (int i = 0; i < message.responses.Count; i++) {
			//ADDS PARENT BELIEF FOR RESPONSE IF IT ISN'T MARKED AS ANOTHER BELIEF ID
			if (message.responses[i].belief != null) {
				if(message.responses[i].belief.Contains("none")) {
						message.responses[i].belief = message.belief;
					}
			}
				msg.GetComponent<ViewMessage>().addResponse(message.responses[i]);
		}

	}
	/*
	private string[] getPath(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}
*/
}
