using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int tan = 0;
	public int fitness = 0;
	public int style = 0;
	public int actionsLeft = 0;
	public int daysLeft = 0;
	public bool reset = false;

	// Use this for initialization
	void Start () {
		//for debugging, reset the player stats
		if (reset) {
			PlayerPrefs.DeleteKey("game in progress");
		}

		if (PlayerPrefs.HasKey("game in progress")) {
			//Game in Progress, populate variables
		}
		else {
			//New game, reset variables
			PlayerPrefs.SetInt("game in progress", 1);
			PlayerPrefs.SetInt("tan", Random.Range(0,10));
			PlayerPrefs.SetInt("fitness", Random.Range(0,10));
			PlayerPrefs.SetInt("style", Random.Range(0,10));
			PlayerPrefs.SetInt("actions left", 3);
			PlayerPrefs.SetInt("days left", 30);
		}
		populateStats();
	}

	private void populateStats() {
		tan = PlayerPrefs.GetInt("tan", 0);
		fitness = PlayerPrefs.GetInt("fitness", 0);
		style = PlayerPrefs.GetInt("style", 0);
		actionsLeft = PlayerPrefs.GetInt("actions left", 0);
		daysLeft = PlayerPrefs.GetInt("days left", 0);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
