using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarOption : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetImage() {
		AvatarOptions options = gameObject.GetComponentInParent<AvatarOptions>();
		options.image.sprite = gameObject.GetComponent<Image>().sprite;
		options.image.enabled = true;
	}
}
