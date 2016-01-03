using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarColorOption : MonoBehaviour {
	
	public void selected() {
		GetComponentInParent<AvatarColorSelector>().character.hair = GetComponent<Image>().color;
//		gameObject.transform.parent.parent.GetComponent<ImageSet>().load ();
//		gameObject.transform.parent.parent.GetComponent<ImageSet>().set ();
	}

	public void setHair() {
		GetComponentInParent<AvatarColorSelector>().character.hair = GetComponent<Image>().color;
		GetComponentInParent<AvatarColorSelector>().character.setHairColor();
	}

	public void setSkin() {
		GetComponentInParent<AvatarColorSelector>().character.skin = GetComponent<Image>().color;
		GetComponentInParent<AvatarColorSelector>().character.setBaseSkinTone();

	}
}
