using UnityEngine;
using System.Collections;

public class RemoveAvatarOption : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void remove() {
		gameObject.transform.parent.parent.GetComponent<ImageSet>().sprite.enabled = false;
	}
}
