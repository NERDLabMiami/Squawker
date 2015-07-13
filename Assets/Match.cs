using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Match : MonoBehaviour {
	public Player player;
	public Text alias;
	public Text motto;
	public Image tempImage;
	public Sprite[] avatars;
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
		alias.text = player.fetchMaleName();
		motto.text = player.fetchMotto();
		tempImage.sprite = avatars[Random.Range (0,avatars.Length)];
		// Intro randomizer
	}

	public void wink() {
		//TODO: Get Minimimum Required Attributes
		//TAN > player.tan
		//love <= player.love
		//accessory = accessory match
		if (player.takeAction()) {
			GetComponent<Animator>().SetTrigger("winked");
			player.updateProfile();
			//TODO: Instead of passing to create a new character, it should log it in the system
			pass ();
		}
	}
}
