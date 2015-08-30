using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TanningBed : MonoBehaviour {
	public float tanAmount = 0;
	public Text feedbackMessage;
	public Player player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startTanningSlider() {
		Time.timeScale = 1f;
	}

	public void stopTanningSlider() {
		//TODO: take into account severity and their risk level
		//TODO: add sun spots

		player.newOffer ("dermatologist");

		switch((int)tanAmount) {
		case 0:
			feedbackMessage.text = "Worst tan ever!";
			break;
		case 1:
			feedbackMessage.text = "Not too pale";
			break;
		case 2:
			feedbackMessage.text = "Nice and orange!";
			break;
		case 3:
			feedbackMessage.text = "BURNED!";
			break;	
		}
		Time.timeScale = 0f;
		int tan = player.tan + (int)tanAmount;
		player.setTan(tan);
		player.takeAction();


	}

	public void setTanAmount(float t) {
		tanAmount = t;
	}
}
