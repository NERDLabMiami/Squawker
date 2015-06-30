using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDebug : MonoBehaviour {
	public Text daysLeft;
	public Text actionsLeft;
	public PlayerStats stats;

	// Use this for initialization
	void Start () {
		refresh();
	}

	public void refresh() {
		daysLeft.text = stats.daysLeft.ToString();
		actionsLeft.text = stats.actionsLeft.ToString();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
