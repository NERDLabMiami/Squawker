using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Response  {
	public string path;
	public string text;
	public string belief = "";
	public int responseTime;
	public int messageIndex;

	//	public Player player;
	//	private Inbox inbox;
	//	public Text response;

	public Response(string p, string txt, int rt, int index, string b) {
		responseTime = rt;
		path = p + "/" + responseTime;
		text = txt;
		belief = b;
		messageIndex = index;
	}

}
