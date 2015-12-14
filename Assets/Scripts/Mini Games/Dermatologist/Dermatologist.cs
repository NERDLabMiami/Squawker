using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dermatologist : MonoBehaviour {
	private int conversationIndex = 0;
	private int dermatologistVisits;
	public Text dermatologistDialog;
	public Text playerDialog;
	public Player player;

	// Use this for initialization
	void Start () {
		conversationIndex = 0;
		dermatologistVisits = PlayerPrefs.GetInt("dermatologist visits",0);
		if (dermatologistVisits > 0) {
			dermatologistDialog.text = "You again? Another skin exam?";
		}
		dermatologistVisits++;
		PlayerPrefs.SetInt("dermatologist visits", dermatologistVisits);

	}

	public void respond() {
		//TODO: Keep track of visits to the dermatologist
		//TODO: With assets in place, transition between close up of dermatologist and player's face
		//TODO: With assets in place, animate transitions
		if (conversationIndex == 4) {
			player.visitDermatologist();
			player.loadSceneNumber (1);
		}
		else {
			dermatologistDialog.text = player.getDermatologistMessage(conversationIndex);
			playerDialog.text = player.getDermatologistResponse(conversationIndex);
			conversationIndex++;
		}
	}

}
