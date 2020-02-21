using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using SimpleJSON;
using System;
using System.Collections.Generic;

public class IncomingMessage : MonoBehaviour {
	public string character;
	public Character avatar;
    public Text body;
    private Message message;

	void Start() {

	}

	public void setMessage(Message msg) {
		message = msg;
		character = message.sender;
		body.text = msg.body;
	}


	/*
	private string[] getPath(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}
*/
}
