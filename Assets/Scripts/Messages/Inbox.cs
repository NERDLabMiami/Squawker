using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class Inbox : MonoBehaviour {
	public IncomingMessage messageTemplate;
	public GameObject messageContainer;

	void Start () {
	}
	
	public void clear() {
		foreach (Transform child in messageContainer.transform)
		{
			Destroy(child.gameObject);
		}		
	}

	public void addMessage(Message msg)  {
		GameObject message = Instantiate(messageTemplate.gameObject);
		message.GetComponent<IncomingMessage>().setMessage(msg);
		message.transform.SetParent(messageContainer.transform, false);
	}
}
