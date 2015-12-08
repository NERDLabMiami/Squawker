using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TanningSalonAssistant : MonoBehaviour {

	public Text dialogue;
	public Text response;
	public Player player;
	public GameObject waiver;
	public GameObject tanControls;
	public GameObject dialogControls;
	public Animator bed;

	private int conversationIndex = 0;
	private bool signedWaiver = false;
	// Use this for initialization
	void Start () {
	}

	public void respond() {
		dialogue.text = player.getTanningSalonAssistantMessage (conversationIndex);
		response.text = player.getTanningSalonAssistantResponse (conversationIndex);
		conversationIndex++;
		if (conversationIndex == 3) {
			gameObject.SetActive(false);
			waiver.SetActive(true);
		}
		if (conversationIndex == 4) {
			if (signedWaiver) {
				dialogControls.SetActive(false);
				Debug.Log("Activating Salon");
				tanControls.SetActive(true);
				bed.SetTrigger("close");
			}
			else {
				Application.LoadLevel(1);
			}
		}
		if (conversationIndex == 5) {
			Time.timeScale = 1f;

			Application.LoadLevel(1);
		}
	}

	public void accept() {
		//TODO: Animate transition between waiver
		waiver.SetActive (false);
		gameObject.SetActive (true);
		signedWaiver = true;
		bed.SetTrigger("open");
	}

	public void decline() {
		//TODO: Should log analytics for declining offer
		//TODO: Animate the transition
		dialogue.text = "Changed your mind? That's okay. Maybe next time!";
		response.text = "Ok!";
		waiver.SetActive (false);
	}

}
