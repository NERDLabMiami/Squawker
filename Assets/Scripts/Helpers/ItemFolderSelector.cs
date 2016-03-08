using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemFolderSelector : MonoBehaviour {
//	public GameObject[] items;
	public Image avatarImage;

	public GameObject itemPrototype;
	public string path;
	// Use this for initialization
	void Start () {
//		RectTransform rect = GetComponent<RectTransform>();
//		rect.offsetMin = new Vector2(rect.offsetMin.x, 300);

		Sprite[] sprites = Resources.LoadAll <Sprite> (path);
		for (int i = 0; i < sprites.Length; i++) {
			GameObject item = (GameObject)Instantiate(itemPrototype);
			item.GetComponent<ItemSelector>().selectButton.image.sprite = sprites[i];
			item.transform.SetParent(gameObject.transform,false);
			if (avatarImage.enabled) {
				if (avatarImage.sprite.name == sprites[i].name) {

					item.GetComponent<ItemSelector>().use();
				}
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void assignImage(string p) {
		Debug.Log("Loading Image at " + p);
		avatarImage.sprite = Resources.Load<Sprite>(path + p);
		avatarImage.enabled = true;
	}

	public void removeImage() {
		avatarImage.enabled = false;
		//TODO: Loop through other items and check for inUse to remove those as well.
	}
}
