using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Message {
	public string sender;
	public string path;
	public string passage;
	public string body;
	public string belief = "";
	public int index;
	public List<Response> responses;

	public Message() {
		responses = new List<Response>();

	}
}
