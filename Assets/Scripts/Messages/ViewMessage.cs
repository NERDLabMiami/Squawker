using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewMessage : MonoBehaviour {
	public Image profilePic;
	public Text sender;
	public Text message;
	public GameObject[] responses;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable() {
		Debug.Log("Destroying Object");
		GameObject.FindGameObjectWithTag("Inbox").GetComponent<Inbox>().messageContainer.SetActive(true);
		Destroy (this.gameObject);
	}
}
