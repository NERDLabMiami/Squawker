using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class IncomingMessage : MonoBehaviour {
	public Text sender;
	public Text subject;
	public ViewMessage expandedMessageTemplate;

	private JSONNode json;
	private Message message;

	public void setMessage(Message msg) {
		Debug.Log("S MESSAGE: " + msg);
		message = msg;
		sender.text = message.sender;
		subject.text = message.subject;
		Debug.Log("Message Sender: " + message.sender);
	}

	public void show() {

		GameObject msg = Instantiate(expandedMessageTemplate.gameObject);
		msg.GetComponent<ViewMessage>().sender.text = message.sender;
		msg.GetComponent<ViewMessage>().body.text = message.body;
		for (int i = 0; i < message.responses.Count; i++) {
			msg.GetComponent<ViewMessage>().addResponse(message.responses[i]);
		}

		msg.transform.SetParent(this.gameObject.transform.parent.parent, false);
		this.transform.parent.gameObject.SetActive(false);
	}

	private string[] getPath(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}

}
