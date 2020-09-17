using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {

	public InputField playerId;

    [System.Obsolete]
    public IEnumerator submitPlayer(string skin, string hair, string face, string ears, string eyes, string iris, string mouth, string nose) {
		WWWForm form = new WWWForm ();
		form.AddField("qualtrics", playerId.text);
		form.AddField ("skin", skin);
		form.AddField ("hair", hair);
		form.AddField ("face", face);
		form.AddField ("ears", ears);
		form.AddField ("eyes", eyes);
		form.AddField ("iris", iris);
		form.AddField ("mouth", mouth);
		form.AddField ("nose", nose);
        WWW link = new WWW("http://track.nerdlab.miami/player.php", form);
		yield return link;
		if (!string.IsNullOrEmpty(link.error)) {
			Debug.Log (link.error);
		}
		else {
			Debug.Log ("Submitted New Player");
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
