using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Response  {
	public string path;
	public string text;
	public string belief = "";
	public int messageIndex;

	//	public Player player;
	//	private Inbox inbox;
	//	public Text response;

	public Response(string p, string txt, int index, string b) {
        path = p;
		text = txt;
		belief = b;
		messageIndex = index;
	}

}
