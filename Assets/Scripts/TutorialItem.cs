using UnityEngine;
using System.Collections;

public class TutorialItem : MonoBehaviour {
	public Tutorial tutorial;

	public void next() {
		gameObject.SetActive(false);
		tutorial.next ();
	}
}
