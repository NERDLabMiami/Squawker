using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {
	public Player player;
	public GameObject introMessage;
	public GameObject gameOverMessage;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("tutorial", 0) == 0) {
			introMessage.SetActive (true);
			PlayerPrefs.SetInt ("tutorial", 1);
		} 
	}

	// Update is called once per frame
	void Update () {
	
	}
}
