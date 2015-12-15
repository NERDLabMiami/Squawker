using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;


public class BehaviorTracking : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void trackNewGame() {
		Dictionary<string, string> game = new Dictionary<string, string> {

			{"player_id", "abcdefg"},
			{"something", "else"}
		};
			
		ParseAnalytics.TrackEventAsync("NewGame", game);
	}
}
