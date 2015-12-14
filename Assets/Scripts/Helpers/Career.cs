using UnityEngine;
using System.Collections;


public class Career : MonoBehaviour {
	public Player player;
	public GameObject avatarCreator;
	// Use this for initialization

	void Start () {
	}
	
	public void startMonth() {
		avatarCreator.SetActive(true);
}

	public void continueMonth() {
		player.loadSceneNumber (1);
	}
}
