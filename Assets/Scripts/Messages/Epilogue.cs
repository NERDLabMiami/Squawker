using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Epilogue : MonoBehaviour {
	public Character player;
	public Character npc;
	public Player playerStats;
	public Text story;
	public AudioClip sadEnding;
	public AudioClip middleEnding;
	public AudioClip happyEnding;
	public int endingType = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void cue() {
		//TODO: Check player's cancer risk
		bool playerHasCancer = false;
		if (playerStats.cancerRisk > 0) {
			//TODO: Figure out proper odds

			int riskFactor = Random.Range(playerStats.cancerRisk, 10);
			if (riskFactor >= 5) {
				//IF PLAYER TANNED 5 TIMES, THEY WILL GET CANCER
				//IF PLAYER TANNED 4 TIMES, THEIR ODDS ARE 5:6
				//IF PLAYER TANNED 3 TIMES, THEIR ODDS ARE 4:6
				//HAS CANCER
				playerHasCancer = true;
			}

		}
		gameObject.SetActive (true);
		npc.assign ();
		switch (npc.characterType) {
		case "C11":
		case "C12":
		case "C13":
			story.text = story.text + " For a short while, things were great. ";
			story.text = story.text + "Then came the news. " + npc.characterName + " had skin cancer. ";
			if (playerHasCancer) {
				story.text = story.text + " You decided to get a skin exam for yourself and the doctor found a malignant mole. You caught it in time, but for " + npc.characterName + " the same couldn't be said.";
			} else {
				story.text = story.text + npc.characterName + " didn't make it. You never found someone that made you that happy ever again."; 
			}
			break;
		case "C21":
		case "C22":
		case "C23":
			if (playerHasCancer) {
				story.text = story.text + " For some time, things were great. Then one day, " + npc.characterName + " found a suspicious mole on your body. ";
				story.text = story.text + "It was malignant. You were diagnosed with skin cancer. Fortunately, " + npc.characterName + " stayed with you until the end.";
			} else {
				story.text = story.text + " The two of you lived a long and healthy life together.";
			}
			break;
		case "C31":
		case "C32":
		case "C33":
			story.text = story.text + " Then one day, " + npc.characterName + " was diagnosed with skin cancer. Thankfully it was caught early and the cancer is now in remission.";
			if (playerHasCancer) {
				story.text = story.text + " The same could not be said about you. Your skin cancer was aggressive and you did not win the fight. " + npc.characterName + " stayed with you until the end.";
			} else {
				story.text = story.text + " The two of you lived a long and happy life together.";
			}
			break;
		}

	}
}
