using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Epilogue : MonoBehaviour {
	public Character player;
	public Character npc;
	public Player playerStats;
	public Text story;
	public string lover;
	public AudioClip sadEnding;
	public AudioClip middleEnding;
	public AudioClip happyEnding;
	public AudioSource source;
	public int endingType = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void cue() {
		gameObject.SetActive (true);
		npc.assign ();
		source.Stop ();
		switch (endingType) {
		case 1:
				//Healthy
			story.text = story.text + " The two of you lived a long and happy life together.";
			source.PlayOneShot (happyEnding);
				break;
			case 2:
				//NPC gets cancer but survives, player remains healthy
				story.text = story.text + " Then one day, " + lover + " was diagnosed with skin cancer. Thankfully it was caught early and the cancer is now in remission.";
				source.PlayOneShot (middleEnding);
				break;
			case 3:
				//NPC gets cancer and dies, player remains healthy
				story.text = story.text + " For a short while, things were great. ";
				story.text = story.text + "Then came the news. " + lover + " had skin cancer. You stayed with %C until the end, but you never found a love like that again.";
				source.PlayOneShot (sadEnding);
				break;
			case 4:
				//Both get cancer, both survive
				story.text = story.text + " For a short while, things were great. ";
				story.text = story.text + "Then came the news. " + lover + " had skin cancer. ";
				story.text = story.text + " You decided to get a skin exam for yourself and the doctor found a malignant mole. Luckily, you both caught it in time. The two of you had a tough but good life together.";
				source.PlayOneShot (middleEnding);
				break;
			case 5:
				//Both get cancer, NPC dies
				story.text = story.text + " For a short while, things were great. ";
				story.text = story.text + "Then came the news. " + lover + " had skin cancer. ";
				story.text = story.text + " You decided to get a skin exam for yourself and the doctor found a malignant mole. You caught it in time, but for " + lover + " the same couldn't be said.";
				source.PlayOneShot (sadEnding);
				break;
			case 6:
				//Player gets cancer, NPC does not.
				story.text = story.text + " For some time, things were great. Then one day, " + lover + " found a suspicious mole on your body. ";
				story.text = story.text + "It was malignant. You were diagnosed with skin cancer. Fortunately, " + lover + " stayed with you until the end.";
				source.PlayOneShot (middleEnding);
				break;
			default:
					story.text = story.text + " The two of you lived a long and healthy life together.";
					source.PlayOneShot (happyEnding);
				break;			
		}

	}
}
