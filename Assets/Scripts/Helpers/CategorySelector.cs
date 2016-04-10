using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CategorySelector : MonoBehaviour {
	public GameObject pageContainer;
	public GameObject selectedPage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void select() {

//		GameObject[] pages = pageContainer.transform.GetComponentsInChildren<GameObject> ();
		Button[] btns = transform.parent.GetComponentsInChildren<Button>();
		for (int i = 0; i < btns.Length; i++) {
			btns[i].interactable = true;
		}
		GetComponent<Button>().interactable = false;
		for (int i = 0; i < pageContainer.transform.childCount; i++) {
			pageContainer.transform.GetChild(i).gameObject.SetActive(false);
		}

		selectedPage.SetActive (true);
	}
}
