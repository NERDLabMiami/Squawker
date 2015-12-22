using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupidStats : MonoBehaviour {
	public int healed;
	public Text healCount;
	private CupidControl cupidControl;

	// Use this for initialization
	void Start () {
		cupidControl = gameObject.GetComponent<CupidControl>();

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void heal() {
		GetComponent<AudioSource> ().Play ();
		healed++;
		healCount.text = healed.ToString();

	}

	public void broken() {
		cupidControl.endGame(healed);
	}
}
