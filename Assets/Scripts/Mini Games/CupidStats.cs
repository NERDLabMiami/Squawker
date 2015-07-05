using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupidStats : MonoBehaviour {
	private int healed;
	public Text healCount;
	public Text feedback;
	private CupidControl cupidControl;

	// Use this for initialization
	void Start () {
		cupidControl = gameObject.GetComponent<CupidControl>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void heal() {
		healed++;
		healCount.text = healed + " Hearts Healed";

	}

	public void broken() {
		feedback.text = "a heart remains broken";
		cupidControl.endGame(healed);
	}
}
