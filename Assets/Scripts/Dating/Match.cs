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

		if(player.takeAction(true)) {
			newMatch();
			player.updateProfile();

		}
	}

	public void newMatch() {
		avatar.randomlyGenerate ();
		string matchGender = PlayerPrefs.GetString ("gender preference", "both");
		avatar.assignName(matchGender);
		alias.text = avatar.name;

	}

	public void wink() {
		avatar.assignCharacter(PlayerPrefs.GetString ("gender preference", "both"));
		avatar.saveCharacter();
		if (player.takeAction(true)) {

			Debug.Log("adding message for " + avatar.getCharacterAssignment() + " with name of " + avatar.characterName);
			int timeAdded = player.matches(avatar.getCharacterAssignment());
			player.addMessage(avatar.getCharacterAssignment() + "/intro/" + timeAdded);
			player.updateProfile();
			Invoke("resetName",3);
		}


	}

	private void resetName() {
		alias.text = "Dream Date";
	}
}
