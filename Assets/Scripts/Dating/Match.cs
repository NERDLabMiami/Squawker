using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Match : MonoBehaviour {
	public Player player;
	public Text alias;
	public Character avatar;
	private string path;

	// Use this for initialization

	public void newMatch() {
		avatar.randomlyGenerate ();
		string matchGender = PlayerPrefs.GetString ("gender preference", "both");
		avatar.assignName(matchGender);
		alias.text = avatar.name;
		if(player.takeAction(true)) {
			player.updateProfile ();
		}
		player.refresh();

	}

	public void wink() {
		string gender = PlayerPrefs.GetString ("gender preference", "both");
		bool charactersAvailable = false;
		bool fizz = false;
		// TODO: Check to see if this can come later to allow for more matches if they don't match well
		if (!player.hooked ()) {
			if (avatar.assignCharacter (gender) > 0) {
				avatar.saveCharacter ();
				charactersAvailable = true;
			}
		} else {
			//GENERATE INTEREST FROM A NON-TAN PERSONA
			if(avatar.assignFizzle() > 0) {
				avatar.saveCharacter();
				charactersAvailable = true;
				fizz = true;
			}
		}

		//TODO: Check if the player has a conversation with tanning involved

		if (player.takeAction(true)) {
			if (charactersAvailable & !fizz) {
				int timeAdded = player.matches (avatar.getCharacterAssignment ());
				if (timeAdded < 50) {
					//NPC with tanning intention now exists
					player.addMessage(avatar.getCharacterAssignment() + "/intro/" + timeAdded);
					Debug.Log ("Adding NPC with tanning beliefs. Hooking player.");
					player.hook ();
				}
			}
			else if (fizz) {
				player.addMessage(avatar.getCharacterAssignment() + "/intro/" + 1);
			}
			player.refresh();
			player.updateProfile();
			Invoke("resetName",3);
		}


	}

	private void resetName() {
		alias.text = "";
	}
}
