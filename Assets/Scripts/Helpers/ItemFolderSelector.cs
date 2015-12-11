using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemFolderSelector : MonoBehaviour {
//	public GameObject[] items;
	public Image avatarImage;

	public GameObject itemPrototype;
	public string path;
	public int maxColumns = 4;
	// Use this for initialization
	void Start () {
//		RectTransform rect = GetComponent<RectTransform>();
//		rect.offsetMin = new Vector2(rect.offsetMin.x, 300);

		Sprite[] sprites = Resources.LoadAll <Sprite> (path);
		int col = 0;
		int row = 0;
		for (int i = 0; i < sprites.Length; i++) {
			if (col >= maxColumns) {
				col = 0;
				row++;
			}
			GameObject item = (GameObject)Instantiate(itemPrototype);

			item.transform.SetParent(gameObject.transform,false);
			item.transform.localScale = new Vector3(1,1,1);
			Vector3 position = new Vector3(gameObject.transform.position.x + (col*120),gameObject.transform.position.y - (row*120),gameObject.transform.position.z);
			item.transform.position = position;
			item.GetComponent<ItemSelector>().selectButton.image.sprite = sprites[i];
			if (avatarImage.sprite.name == sprites[i].name && avatarImage.enabled) {

				item.GetComponent<ItemSelector>().use();
			}
			col++;

		}
		GameObject spacer = new GameObject();
		spacer.transform.SetParent(gameObject.transform,false);
		spacer.transform.localScale = new Vector3(1,1,1);
		Vector3 pos = new Vector3(0, gameObject.transform.position.y - ((row + 1) * 200), gameObject.transform.position.z);
		spacer.transform.position = pos;

		//rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottom);
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
