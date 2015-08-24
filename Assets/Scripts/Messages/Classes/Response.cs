using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Response  {
	public string path;
	public string text;
	public int responseTime;
	public int messageIndex;

	//	public Player player;
	//	private Inbox inbox;
	//	public Text response;

	public Response(string p, string txt, int rt, int index) {
		responseTime = rt;
		path = p + "/" + responseTime;
		text = txt;
		messageIndex = index;
	}

}
