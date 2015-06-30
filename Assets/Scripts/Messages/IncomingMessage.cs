using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class IncomingMessage : MonoBehaviour {
	public Text sender;
	public Text message;
	public ViewMessage expandedMessageTemplate;
	public string path;
	public int index;
	private string character;
	private string passage;
	private JSONNode json;

	public void show() {
		//get path
		string[] splitPath = path.Split (new string[] {"/"}, System.StringSplitOptions.None);
		character = splitPath[0];
		passage = splitPath[1];
		json = JSON.Parse(GetComponentInParent<Inbox>().characters.ToString());

		//populate character
		sender.text = character;

		//create full message
		expandedMessageTemplate.sender.text = character;
		expandedMessageTemplate.message.text = json[character][passage]["message"];
		for(int i = 0; i < json[character][passage]["responses"].Count; i++) {
			Debug.Log("Response #" + i);
			expandedMessageTemplate.responses[i].GetComponent<Response>().response.text =  json[character][passage]["responses"][i]["response"];
			expandedMessageTemplate.responses[i].GetComponent<Response>().path =  json[character][passage]["responses"][i]["path"] + "/" + json[character][passage]["responses"][i]["time"];
			expandedMessageTemplate.responses[i].GetComponent<Response>().messageIndex = index;
			//			msg.GetComponent<ViewMessage>().responses[i].GetComponent<Button>().onClick.AddListener(
			//TODO: If there are previous longer response lists, it will show up	
		}


		GameObject msg = Instantiate(expandedMessageTemplate.gameObject);
		msg.transform.SetParent(this.gameObject.transform.parent.parent, false);
		this.transform.parent.gameObject.SetActive(false);
	}

	private string[] getPath(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}

}
