using UnityEngine;
using System.Collections;


public class Career : MonoBehaviour {
	public Player player;
	public GameObject avatarCreator;
	public GameObject continueButton;
	// Use this for initialization

	void Start () {
		int started = PlayerPrefs.GetInt("game in progress", 0);
		if (started == 1) {
			continueButton.SetActive(true);
		}

	}
	
	public void startMonth() {
		avatarCreator.SetActive(true);
}

	public void continueMonth() {
		player.loadSceneNumber (1);
	}
}
