using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class ItemFolderSelector : MonoBehaviour {
	public GameObject[] items;
	public Image avatarImage;

	public GameObject itemPrototype;
	public string path;
	public int maxColumns = 4;
	// Use this for initialization
	void Start () {
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
			Vector3 position = new Vector3(gameObject.transform.position.x + (col*100),gameObject.transform.position.y - (row*100),gameObject.transform.position.z);
			item.transform.position = position;
			item.GetComponent<ItemSelector>().selectButton.image.sprite = sprites[i];
			if (AssetDatabase.GetAssetPath(avatarImage.sprite) == AssetDatabase.GetAssetPath (sprites[i]) && avatarImage.enabled) {

				item.GetComponent<ItemSelector>().use();
			}

			col++;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void assignImage(string path) {
		avatarImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite> (path);
		avatarImage.enabled = true;
	}

	public void removeImage() {
		avatarImage.enabled = false;
	}
}
