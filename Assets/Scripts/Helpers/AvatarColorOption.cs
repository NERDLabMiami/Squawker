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
		Button[] btns = transform.parent.GetComponentsInChildren<Button>();
		for(int i = 0; i < btns.Length; i++) {
			btns[i].interactable = true;
		}
		GetComponent<Button>().interactable = false;
	}

	public void setSkin() {
		GetComponentInParent<AvatarColorSelector>().character.skin = GetComponent<Image>().color;
		GetComponentInParent<AvatarColorSelector>().character.setBaseSkinTone();
		Button[] btns = transform.parent.GetComponentsInChildren<Button>();
		for(int i = 0; i < btns.Length; i++) {
			btns[i].interactable = true;
		}
		GetComponent<Button>().interactable = false;
	}
}
