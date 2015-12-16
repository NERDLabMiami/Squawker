using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
//	private GameObject[] items;
	private TutorialItem[] items;
	private int tutorialIndex = 0;

	public void next() {
		tutorialIndex++;
		if (tutorialIndex < items.Length) {
			items[tutorialIndex].gameObject.SetActive(true);
		}
		else {
			gameObject.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		items = GetComponentsInChildren<TutorialItem>(true);
		items[tutorialIndex].gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
