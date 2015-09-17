using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TanningSalonAssistant : MonoBehaviour {

	public Text dialogue;
	public Text response;
	public Player player;
	public GameObject waiver;
	public GameObject salon;

	private int conversationIndex = 0;

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
			gameObject.transform.parent.gameObject.SetActive(false);
			salon.SetActive(true);
		}
		if (conversationIndex == 5) {
			Application.LoadLevel(1);
		}
	}

	public void accept() {
		//TODO: Animate transition between waiver
		waiver.SetActive (false);
		gameObject.SetActive (true);
	}

	public void decline() {
		//TODO: Should log analytics for declining offer
		//TODO: Animate the transition
		Application.LoadLevel (1);
	}

}
