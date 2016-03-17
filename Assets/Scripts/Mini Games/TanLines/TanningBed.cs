using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TanningBed : MonoBehaviour {
	public float tanAmount = 0;
	public TanningSalonAssistant assistant;
	public Character character;
	public GameObject stopTanButton;
	public GameObject continueButton;
	public int visitsRequiredForMole = 2;
	private int salonVisits;
	// Use this for initialization
	void Start () {
		salonVisits = PlayerPrefs.GetInt("tanning salon visits",0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startTanningSlider() {
	}

	public void stopTanningSlider() {
		//TODO: take into account severity and their risk level
		//TODO: add sun spots
		Time.timeScale = 0f;
		GetComponent<AudioSource> ().Stop ();
		continueButton.SetActive (true);
		stopTanButton.SetActive (false);
		salonVisits++;
		PlayerPrefs.SetInt ("tanning salon visits", salonVisits);
		assistant.player.newOffer ("exam");
		switch((int)tanAmount) {
		case 0:
			assistant.dialogue.text = "You didn't get much of a tan, but you still look great!";
			assistant.response.text = "Thanks!";
			break;
		case 1:
			assistant.dialogue.text = "Great tan! Come back if you want to keep it that way.";
			assistant.response.text = "Thanks!";
			break;
		case 2:
			assistant.dialogue.text = "Yo Jersey, looking good! That's what I call a tan!";
			assistant.response.text = "Thanks!";
			break;
		case 3:
			assistant.dialogue.text = "Whoa, that looks painful. You shouldn't stay in there that long.";
			assistant.response.text = "I'll have to work on that.";
			break;	
		case 4:
			assistant.dialogue.text = "Wow, looks like you got sunburned. Be careful next time!";
			assistant.response.text = "Sounds good!";
			break;
		}
		int tan = (int)tanAmount;
		//TODO: Send analytics?
		Debug.Log("Setting tan to : " + tan);

//		assistant.player.setTan (tan);
		character.setTone (tan);

		if (salonVisits >= visitsRequiredForMole) {
			character.getMole ();
		}
		assistant.player.tan = tan;
		assistant.player.takeAction (false);
		assistant.dialogControls.SetActive (true);

	}

	public void setTanAmount(float t) {
		tanAmount = t;
	}
}
