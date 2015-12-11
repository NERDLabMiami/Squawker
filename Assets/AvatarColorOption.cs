using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarColorOption : MonoBehaviour {
	public int colorReference;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void selected() {
		GetComponentInParent<AvatarColorSelector>().selectedColor = colorReference;
		gameObject.transform.parent.parent.GetComponent<ImageSet>().load ();
//		gameObject.transform.parent.parent.GetComponent<ImageSet>().sprite.sprite = //gameObject.GetComponent<Image>().sprite; 
		gameObject.transform.parent.parent.GetComponent<ImageSet>().set ();
	}
}
