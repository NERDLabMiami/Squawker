using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ResponseOption : MonoBehaviour {
	public Text response;
	public AudioClip click;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clicked() {
		Camera.main.gameObject.GetComponent<AudioSource> ().PlayOneShot (click);
	}
}
