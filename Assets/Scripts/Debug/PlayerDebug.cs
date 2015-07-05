using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDebug : MonoBehaviour {
	public Text daysLeft;
	public Text actionsLeft;
	public Player player;

	// Use this for initialization
	void Start () {
		refresh();
	}

	public void refresh() {
		if (player.takeAction()) {
			Debug.Log("Action Taken, Refreshing");
			daysLeft.text = player.daysLeft.ToString();
			actionsLeft.text = player.actionsLeft.ToString();

		}
		else {
			Debug.Log("No more actions allowed.");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
