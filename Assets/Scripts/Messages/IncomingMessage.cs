using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class IncomingMessage : MonoBehaviour {
//	public Text alias;
	public string character;
	public Text subject;
	public ViewMessage expandedMessageTemplate;
	public Character avatar;
	public Image overrideImage;

	private JSONNode json;
	private Message message;

	public void setMessage(Message msg) {
		message = msg;
		character = message.sender;
		switch(character) {
			case "tanning":
				overrideImage.enabled = true;
				break;
			case "love":
				overrideImage.enabled = true;
				break;
			case "dermatologist":
				overrideImage.enabled = true;
				break;
			default:
				overrideImage.enabled = false;
				break;
		}
//		alias.text = message.alias;
		message.alias = PlayerPrefs.GetString(character);
		avatar.assign(character);
		subject.text = message.subject;
	}

	public void show() {

		GameObject msg = Instantiate(expandedMessageTemplate.gameObject);
		msg.GetComponent<ViewMessage>().body.text = message.body;

		switch (message.sender) {
		case "tanning":
			msg.GetComponent<ViewMessage>().alias.text = "Rays Tanning Salon";
//			msg.GetComponent<ViewMessage>().profilePic = "tanning";
			msg.GetComponent<ViewMessage>().profilePic.enabled = true;

			break;
		case "love":
			msg.GetComponent<ViewMessage>().alias.text = "LoveQ";
			msg.GetComponent<ViewMessage>().profilePic.enabled = true;

			break;
		case "dermatologist":
			msg.GetComponent<ViewMessage>().alias.text = "Dermafreeze";
			msg.GetComponent<ViewMessage>().profilePic.enabled = true;

			break;
		default:
//			msg.GetComponent<ViewMessage>().character.assign(message.sender);
			msg.GetComponent<ViewMessage>().character.assign(character);
			msg.GetComponent<ViewMessage>().alias.text = msg.GetComponent<ViewMessage>().character.name;

			break;

		}
		msg.transform.SetParent(this.gameObject.transform.parent.parent.parent.parent, false);
		this.transform.parent.gameObject.SetActive(false);

		for (int i = 0; i < message.responses.Count; i++) {
			msg.GetComponent<ViewMessage>().addResponse(message.responses[i]);
		}

	}

	private string[] getPath(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}

}
