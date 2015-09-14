using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Match : MonoBehaviour {
	public Player player;
	public Text alias;
	public Text motto;
	public Character avatar;
	private string path;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pass() {
		//TODO: Generate a new potential match
		// Name randomizer

		if(player.takeAction()) {
			newMatch();
			player.updateProfile();

		}
	}

	public void newMatch() {
		avatar.randomlyGenerate ();
		avatar.assignName("men");
		alias.text = avatar.name;

	}

	public void wink() {
		if (player.takeAction()) {
			GetComponent<Animator>().SetTrigger("winked");
			//TODO: Check if NPC and Player are a match
			//TODO: Get Minimimum Required Attributes
			//TAN > player.tan
			//love <= player.love
			//accessory = accessory match
			//TODO: Instead of passing to create a new character, it should log it in the system
			avatar.assignCharacter("men");
			Debug.Log ("Saving Character...");
			avatar.saveCharacter ();
			Debug.Log("adding message for " + avatar.getCharacterAssignment());
			player.addMessage(avatar.getCharacterAssignment() + "/intro/" + avatar.getCharacterAssignment());
			player.updateProfile();
		}

		newMatch();

	}
}
