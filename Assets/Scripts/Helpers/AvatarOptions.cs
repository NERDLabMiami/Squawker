using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarOptions : MonoBehaviour {

	private Button[] buttons;
	private Sprite[] sprites;
	public string folder;
	public Image image;


	// Use this for initialization
	void Start () {
		if (folder.Length > 0) {
			sprites = Resources.LoadAll<Sprite> (folder);
			buttons = GetComponentsInChildren<Button>();
			for(int i = 0; i < sprites.Length; i++) {
				buttons[i].image.sprite = sprites[i];
			}
		}
	}

	public void removeImage() {
		image.sprite = null;
		image.enabled = false;
		//Button[] btns = transform.parent.GetComponentsInChildren<Button>();
		for(int i = 0; i < buttons.Length; i++) {
			buttons[i].interactable = true;
		}
	}
}
