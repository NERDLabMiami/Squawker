using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class Inbox : MonoBehaviour {
	public IncomingMessage messageTemplate;
	public GameObject messageContainer;
	public Epilogue epilogue;
	public GameObject emptyMailboxMessage;
	private Player player;

	void Start () {
	}

	public void checkIfEmpty() {
		//TODO: Look at player
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	
			if (player.inbox.Count < 1) {
				emptyMailboxMessage.SetActive (true);
			}
			else {
				emptyMailboxMessage.SetActive (false);

			}

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
