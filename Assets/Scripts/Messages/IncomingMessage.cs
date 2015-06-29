using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class IncomingMessage : MonoBehaviour {
	public Text sender;
	public Text message;
	public GameObject expandedMessageTemplate;
	public string path;
	private string character;
	private string passage;
	private JSONNode json;

	public void show() {
		//get path
		string[] splitPath = path.Split (new string[] {"/"}, System.StringSplitOptions.None);
		character = splitPath[0];
		passage = splitPath[1];
		json = JSON.Parse(GetComponentInParent<InboxDebug>().characters.ToString());

		//populate character
		sender.text = character;

		//populate message
//		message.text = json[character][passage]["message"];

		//create full message
		GameObject msg = Instantiate(expandedMessageTemplate);
		msg.transform.SetParent(this.gameObject.transform.parent, false);

		//set sender and message
		msg.GetComponent<ViewMessage>().sender.text = character;
		msg.GetComponent<ViewMessage>().message.text =  json[character][passage]["message"];

		//populate responses
		for(int i = 0; i < json[character][passage]["responses"].Count; i++) {
			Debug.Log("Response #" + i);
			msg.GetComponent<ViewMessage>().responses[i].GetComponent<Response>().response.text =  json[character][passage]["responses"][i]["response"];
		}



		gameObject.SetActive(false);
	}

	private string[] getPath(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}

}
