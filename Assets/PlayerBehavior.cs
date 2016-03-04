using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {

	public InputField playerId;

	public IEnumerator submitPlayer() {
		WWWForm form = new WWWForm ();
		form.AddField("qualtrics", playerId.text);
		form.AddField ("skin", 2);
		form.AddField ("face", "face.png");
		form.AddField ("ears", "ears.png");
		form.AddField ("eyes", "eyes.png");
		form.AddField ("mouth", "mouth.png");
		form.AddField ("nose", "nose.png");
		WWW link = new WWW ("http://track.nerdlab.miami/player.php", form);
		yield return link;
		if (!string.IsNullOrEmpty(link.error)) {
			Debug.Log (link.error);
		}
		else {
			Debug.Log ("Submitted New Player");
		}
	}
	public IEnumerator submitEvent(int eventId, string subtype, string beliefId, string characterId) {
		WWWForm form = new WWWForm ();
		form.AddField("qualtrics_id", PlayerPrefs.GetString("qualtrics_id"));
		form.AddField("event_id", eventId);
		if (beliefId.Length > 0) {
			form.AddField("belief_id", beliefId);
		}
		if (characterId.Length > 0) {
			form.AddField("character_id", characterId);
		}
		form.AddField("subtype", subtype);
		form.AddField("days", PlayerPrefs.GetInt("days left",-1));
		form.AddField("risk", PlayerPrefs.GetInt("cancer risk",0));
		form.AddField("tan",PlayerPrefs.GetInt("tan",0));
		form.AddField("attractiveness", PlayerPrefs.GetInt("attractiveness"));
		WWW link = new WWW ("http://track.nerdlab.miami/event.php", form);
		yield return link;
		if (!string.IsNullOrEmpty(link.error)) {
			Debug.Log (link.error);
		}
		else {
			Debug.Log ("Submitted Event");
		}
	}

	public void trackEvent(int eventId, string subtype, string beliefId, string characterId) {
		Debug.Log("EVENT: " + eventId);
		Debug.Log("SUBTYPE: " + subtype);
		Debug.Log("BELIEF: " + beliefId);
		Debug.Log("CHARACTER ID: " + characterId);

		StartCoroutine(submitEvent(eventId, subtype, beliefId, characterId));	
	}

	public void newPlayer() {
		PlayerPrefs.SetString("qualtrics_id", playerId.text);
		StartCoroutine (submitPlayer ());
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
