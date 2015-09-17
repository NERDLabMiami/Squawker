using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {
	public Player player;
	public Match match;
	// Use this for initialization
	void Start () {
		player.profile.character.assign("me");
//		player.refreshInbox();
//		player.updateProfile();
		match.newMatch();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
