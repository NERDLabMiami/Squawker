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
		switch(conversationIndex) {
			case 0:
				dermatologistDialog.text = "Great, let me take a look at you.";
				playerDialog.text = "OK";
				conversationIndex++;
				break;
			case 1:
				dermatologistDialog.text = "Looks like you've got some sun spots. It's okay to get some sun, just put on sunscreen.";
				playerDialog.text = "You're right, I should do that.";
				conversationIndex++;
				break;
			case 2:
				dermatologistDialog.text = "I can try freezing them away, would you like me to try?";
				playerDialog.text = "Sure, thanks!";
				conversationIndex++;
				break;
			case 3:
				dermatologistDialog.text = "OK, you're all done. Remember to avoid tanning beds!";
				playerDialog.text = "Will do!";
				conversationIndex++;
				break;
			case 4:
				Application.LoadLevel(0);
				break;
			}
	}

}
