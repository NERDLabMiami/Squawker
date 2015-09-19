using UnityEngine;
using System.Collections;

public class Epilogue : MonoBehaviour {
	public Character player;
	public Character npc;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void cue() {
		gameObject.SetActive (true);
		npc.assign ();
		//TODO: Figure out if it's a happy or sad ending
		Debug.Log ("Show story");
	}
}
