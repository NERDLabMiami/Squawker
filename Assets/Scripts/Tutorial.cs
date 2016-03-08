using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
//	private GameObject[] items;
	private TutorialItem[] items;
	private int tutorialIndex = 0;
	public Button[] buttons;

	public void next() {
		tutorialIndex++;
		if (tutorialIndex < items.Length) {
			items[tutorialIndex].gameObject.SetActive(true);
		}
		else {
			for(int i = 0; i < buttons.Length; i++) {
				buttons[i].interactable = true;
			}
			gameObject.SetActive(false);

		}
	}

	// Use this for initialization
	void Start () {
		items = GetComponentsInChildren<TutorialItem>(true);
		items[tutorialIndex].gameObject.SetActive(true);
		for(int i = 0; i < buttons.Length; i++) {
			buttons[i].interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
