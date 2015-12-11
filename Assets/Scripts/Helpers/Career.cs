using UnityEngine;
using System.Collections;


public class Career : MonoBehaviour {
	public Player player;
	public GameObject avatarCreator;
	// Use this for initialization

	void Start () {
	}
	
	public void startMonth() {
/*		player.resetStats();
		player.newOffer("tanning");
		player.newOffer ("love");
		PlayerPrefs.SetInt("game in progress", 1);
		Application.LoadLevel(1);
*/
		avatarCreator.SetActive(true);
}

	public void continueMonth() {
		Application.LoadLevel(1);
	}
}
