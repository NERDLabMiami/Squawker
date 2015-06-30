using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Response : MonoBehaviour {
	public Text response;
	public string path;
	public int responseTime;
	private Inbox inbox;

	public void respond() {
		inbox.newMessage(path);
		Debug.Log("Destroying full message object");
		inbox.messageContainer.SetActive(true);
		Destroy (transform.parent.parent.gameObject);
	}

	// Use this for initialization
	void Start () {
		inbox = GameObject.FindGameObjectWithTag("Inbox").GetComponent<Inbox>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
