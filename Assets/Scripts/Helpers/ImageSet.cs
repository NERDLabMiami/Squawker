using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSet : MonoBehaviour {
	public string folder;
	public string description;
	public Sprite[] sprites;
	public int selected = 0;
	public Image sprite;
	public AvatarColorSelector colorSelector;
	public ImageSet[] pairedSets;
	public bool startWithSpriteEnabled;
	
	// Use this for initialization
	void Start () {

		load ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void load() {
		/*
		if (colorSelector) {
			Debug.Log("Loading Folder " + colorSelector.selectedColor);
			sprites = Resources.LoadAll<Sprite>(folder + "/" + colorSelector.selectedColor);
		}
		else {
			sprites = Resources.LoadAll<Sprite>(folder);
		}
*/
	}
	public void checkForColor() {
		if (colorSelector) {
			colorSelector.gameObject.SetActive(true);
			
		}
	}
	public void set() {
		/*
		if (pairedSets.Length > 0) {
			for(int i = 0; i < pairedSets.Length; i++) {
				if (pairedSets[i].colorSelector.selectedColor != colorSelector.selectedColor) {
					pairedSets[i].colorSelector.selectedColor = colorSelector.selectedColor;
					pairedSets[i].load ();
					if (pairedSets[i].sprite.gameObject.activeSelf) {
						pairedSets[i].set ();
					}
				}
			}
		}
		sprite.sprite = sprites[selected];
		sprite.gameObject.SetActive(true);
		sprite.enabled = true;
		*/
	}

	public Sprite getNextSprite() {
		if (selected + 1 >= sprites.Length) {
			return sprites[0];
		}
		else {
			return sprites[selected+1];
		}
	}

	public Sprite getPreviousSprite() {
		if (selected - 1 <= 0) {
			return sprites[sprites.Length - 1];
		}
		else {
			return sprites[selected - 1];
		}
	}
	public void next() {
		selected++;
		if (selected >= sprites.Length) {
			selected = 0;
		}
		set ();
	}

	public void previous() {
		selected--;
		if (selected < 0) {
			selected = sprites.Length - 1;
		}
		set ();
	}
}
