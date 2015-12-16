using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {
	public Player player;
	public Match match;
	public GameObject introMessage;
	public GameObject gameOverMessage;

	// Use this for initialization
	void Start () {
//		player.profile.character.assign("me");
//		player.refreshInbox();
//		player.updateProfile();
		if (PlayerPrefs.GetInt ("tutorial", 0) == 0) {
			introMessage.SetActive (true);
			PlayerPrefs.SetInt ("tutorial", 1);
		} else {
			match.newMatch();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
