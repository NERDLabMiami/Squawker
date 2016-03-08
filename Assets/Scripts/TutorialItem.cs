using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialItem : MonoBehaviour {
	public Tutorial tutorial;

	public void next() {
		gameObject.SetActive(false);
		tutorial.next ();
	}
}
