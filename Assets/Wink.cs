using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wink : MonoBehaviour {
	public Animator matchPanel;
	public Button winkButton;
	public Button passButton;
	public Button matchButton;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<ParticleSystem>().isStopped) {
			matchPanel.SetTrigger("mystery");
			winkButton.interactable = false;
			passButton.interactable = false;
			matchButton.interactable = true;

			gameObject.SetActive(false);
			Debug.Log("Disabled WInking");

		}
	
	}
}
