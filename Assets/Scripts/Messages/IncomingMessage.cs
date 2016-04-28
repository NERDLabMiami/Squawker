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
			case "tanning":
				overrideImage.sprite = Resources.Load<Sprite>("Salon/ray");
				break;
			case "love":
				overrideImage.sprite = Resources.Load<Sprite>("LoveQ/cupid");
				break;
			case "exam":
				overrideImage.sprite = Resources.Load<Sprite>("Dermatologist/dermatologist");
				break;
			case "piercing":
				overrideImage.sprite = Resources.Load<Sprite>("Home/pierced_larry");
				break;
			case "haircut":
				overrideImage.sprite = Resources.Load<Sprite>("Home/hairstylist_emerald");
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
		case "tanning":
//			GetComponent<PlayerBehavior>().trackEvent(3, StringArrayFunctions.getMessage(message.path)[1], message.belief, "Ray");
			characterSendingMessage = "Ray";
			eventId = 3;
			subtype = StringArrayFunctions.getMessage(message.path)[1];
			msg.GetComponent<ViewMessage>().alias.text = "Rays Tanning Salon";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("Salon/ray");

			break;
		case "love":
//			GetComponent<PlayerBehavior>().trackEvent(3, "LOVE", "none", "Cupid");
			eventId = 3;
			subtype = "Love";
			characterSendingMessage = "Cupid";
			msg.GetComponent<ViewMessage>().alias.text = "LoveQ";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("LoveQ/cupid");

			break;
		case "exam":
			message.belief = "EE";

//			GetComponent<PlayerBehavior>().trackEvent(3, "EXAM", message.belief,"Doctor");
			characterSendingMessage = "Dermatologist";
			subtype = "Exam";
			eventId = 3;
			msg.GetComponent<ViewMessage>().alias.text = "Dermafreeze";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("Dermatologist/dermatologist");
			break;
		case "piercing":
			characterSendingMessage = "Larry";
			eventId = 3;
			subtype = "Piercing";
//			GetComponent<PlayerBehavior>().trackEvent(3, "PIERCING","none", "Larry");
			msg.GetComponent<ViewMessage>().alias.text = "Larry's Piercing Parlor";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("Home/pierced_larry");
			break;
		case "haircut":
//			GetComponent<PlayerBehavior>().trackEvent(3, "HAIRCUT","none", "Emerald");
			subtype = "Haircut";
			eventId = 3;
			characterSendingMessage = "Emerald";
			msg.GetComponent<ViewMessage>().alias.text = "Emerald's Salon";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("Home/hairstylist_emerald");
			break;
		default:
			eventId = 1;
			subtype = "DLG";
			characterSendingMessage = message.sender;
//			GetComponent<PlayerBehavior>().trackEvent(1, "DLG", message.belief, message.sender);
			msg.GetComponent<ViewMessage>().character.assign(message.sender);
			msg.GetComponent<ViewMessage>().character.assign(character);
			msg.GetComponent<ViewMessage>().alias.text = msg.GetComponent<ViewMessage>().character.name;
			msg.GetComponent<ViewMessage>().profilePic.enabled = false;
			msg.GetComponent<ViewMessage>().character.gameObject.SetActive(true);

			break;

		}

//		GetComponent<PlayerBehavior>().trackEvent(eventId, subtype,message.belief,characterSendingMessage);
		player.trackEvent(eventId, subtype,message.belief,characterSendingMessage);
		Debug.Log("Submitting Event");
		//GetComponent<PlayerBehavior>().trackEvent(1, "test","none","silly");

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
