using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ResponseOption : MonoBehaviour {
	public Text response;
    public GameObject playerResponseTemplate;
    public AudioClip click;
	public Button postCommentButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clicked() {
		Debug.Log("Clicked Option, Turning Off Comment Button");
		Camera.main.gameObject.GetComponent<AudioSource> ().PlayOneShot (click);
		//disable comment button
		postCommentButton.interactable = false;

	}
}
