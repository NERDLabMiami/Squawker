using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Match : MonoBehaviour {
	public Player player;
	public Text alias;
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
/*
		if(player.takeAction(true)) {
			newMatch();
			player.updateProfile();

		}
*/
}

	public void newMatch() {
		avatar.randomlyGenerate ();
		string matchGender = PlayerPrefs.GetString ("gender preference", "both");
		avatar.assignName(matchGender);
		alias.text = avatar.name;
		if(player.takeAction(true)) {
			player.updateProfile ();
		}

	}

	public void wink() {
		string gender = PlayerPrefs.GetString ("gender preference", "both");
		bool charactersAvailable = false;
		// TODO: Check to see if this can come later to allow for more matches if they don't match well
		if (!player.hooked ()) {
			if (avatar.assignCharacter (gender) > 0) {
				avatar.saveCharacter ();
				charactersAvailable = true;
			}
		} else {
			//GENERATE INTEREST FROM A NON-TAN PERSONA

		}

		//TODO: Check if the player has a conversation with tanning involved

		if (player.takeAction(true)) {
			if (charactersAvailable) {
				int timeAdded = player.matches (avatar.getCharacterAssignment (), avatar.wearingGlasses (), avatar.wearingHeadwear (), avatar.wearingTie ());
				if (timeAdded < 50) {
					//NPC with tanning intention now exists
					player.addMessage(avatar.getCharacterAssignment() + "/intro/" + timeAdded);
					Debug.Log ("Adding NPC with tanning beliefs. Hooking player.");
					player.hook ();
				}
			}
			player.updateProfile();
			Invoke("resetName",3);
		}


	}

	private void resetName() {
		alias.text = "Dream Date";
	}
}
