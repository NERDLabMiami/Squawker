using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class IncomingMessage : MonoBehaviour {
	public Text alias;
	public string character;
	public Text subject;
	public ViewMessage expandedMessageTemplate;

	private JSONNode json;
	private Message message;

	public void setMessage(Message msg) {
		message = msg;
		character = message.sender;
		alias.text = message.alias;
		subject.text = message.subject;
		Debug.Log("Message Sender: " + message.sender);
	}

	public void show() {

		GameObject msg = Instantiate(expandedMessageTemplate.gameObject);
		msg.GetComponent<ViewMessage>().body.text = message.body;
		msg.GetComponent<ViewMessage>().character.name = message.sender;
		Debug.Log("CHARACTER NAME IS " + message.sender);

		switch (message.sender) {
		case "tanning":
			break;
		case "love":
			break;
		case "dermatologist":
			break;
		default:
			msg.GetComponent<ViewMessage>().character.assign(message.sender);
			msg.GetComponent<ViewMessage>().alias.text = msg.GetComponent<ViewMessage>().character.name;

			break;

		}
		msg.transform.SetParent(this.gameObject.transform.parent.parent, false);
		this.transform.parent.gameObject.SetActive(false);

		for (int i = 0; i < message.responses.Count; i++) {
			msg.GetComponent<ViewMessage>().addResponse(message.responses[i]);
		}

	}

	private string[] getPath(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}

}
