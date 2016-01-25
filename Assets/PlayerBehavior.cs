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

	public void newPlayer() {
		StartCoroutine (submitPlayer ());
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
