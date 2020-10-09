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
	public Image avatar;
    private Message message;
	public Inbox inbox;

	void Start() {
	}

	public void setMessage(Message msg) {
		message = msg;
		character = message.sender;
		body.text = msg.body;
		//rect 80 units per line
		int lines = body.text.Length / 40;
		Debug.Log(lines + " lines of text");
		int height = lines * 40;
		RectTransform rt = transform.GetComponent<RectTransform>();
		Debug.Log("RT currently " + rt.sizeDelta.y + " setting to " + height);
		rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
		inbox.currentY += height;
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
