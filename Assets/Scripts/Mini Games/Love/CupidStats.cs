using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupidStats : MonoBehaviour {
	private int healed;
	public Text healCount;
	public Text feedback;
	public Image attractiveness;
	private CupidControl cupidControl;

	// Use this for initialization
	void Start () {
		cupidControl = gameObject.GetComponent<CupidControl>();
		attractiveness.CrossFadeAlpha (0,5f,true);

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void heal() {
		healed++;
		healCount.text = healed + " Hearts Healed";
		float alpha = Mathf.Min (cupidControl.player.attractiveness * .01f, 1);
		attractiveness.CrossFadeAlpha (alpha,1f,true);

	}

	public void broken() {
		feedback.text = "a heart remains broken";
		cupidControl.endGame(healed);
	}
}
