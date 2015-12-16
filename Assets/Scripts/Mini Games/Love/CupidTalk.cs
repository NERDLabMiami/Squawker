using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupidTalk : MonoBehaviour {
	public string[] introDialogue;
	public string[] endDialogue;
	public Text dialog;
	public GameObject startButton;
	public GameObject continueButton;

	private int dialogIndex = 0;
	private int selectedDialog = 0;

	// Use this for initialization
	void Start () {
		dialog.text = introDialogue [0];
	}

	public void changeDialog(string dialogName) {
		dialogIndex = 0;

		if (dialogName == "intro") {
			selectedDialog = 0;
			dialog.text = introDialogue [dialogIndex];
		}
		if (dialogName == "end") {
			selectedDialog = 1;
			dialog.text = endDialogue [dialogIndex];
		}
	}

	public void advanceDialog() {
		dialogIndex++;

		switch(selectedDialog) {
			case 0:
				if (introDialogue.Length > dialogIndex) {
			
					dialog.text = introDialogue [dialogIndex];
				}
				if (introDialogue.Length - 1 == dialogIndex) {
					continueButton.SetActive (false);
					startButton.SetActive (true);
				}

				break;
			case 1:
				if (endDialogue.Length > dialogIndex) {
					dialog.text = endDialogue [dialogIndex];
				}
				if (endDialogue.Length - 1 == dialogIndex) {
					//				startButton.interactable = true;
				}
				break;
			}

	}

	public void cueEndTalk() {
		dialogIndex = 0;
	}
}
