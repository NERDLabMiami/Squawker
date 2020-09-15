using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using SimpleJSON;
using System;
using System.Collections.Generic;

public class IncomingMessage : MonoBehaviour {
	public string character;
    public Text body;
	public Image bubble;
    private Message message;
	public Inbox inbox;

	void Start() {
	}

	public void setMessage(Message msg) {
		message = msg;
		character = message.sender;
		body.text = msg.body;
	}

	public void scrollToBottom()
	{
		inbox.m_ScrollRect.normalizedPosition = new Vector2(0, 0);

	}
	/*
	private string[] getPath(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}
*/
}
