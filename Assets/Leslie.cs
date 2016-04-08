using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Leslie : MonoBehaviour {
	public Text dialogBox; 
	public GameObject dialogue;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void makeMatch() {
		dialogBox.text = "I think you're going to like this one...";
		dialogue.SetActive(true);
	}
	//TODO:
	//Comment on making a match
	//Comment on updating your style
	//Comment on checking your mail (depends on the amount of mail)
	//Comment on winking
	//Comment on passing

}
