using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour {
	public bool inUse;
	public Button removeButton;
	public Button selectButton;
	public string path;
	public int index;
	// Use this for initialization
	void Start () {
			inUse = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void use() {
		for(int i = 0; i < gameObject.transform.parent.GetComponentsInChildren<ItemSelector>().Length; i++){
			ItemSelector item = gameObject.transform.parent.GetComponentsInChildren<ItemSelector>()[i];
			if (item.inUse) {
				item.remove();
			}
		}
		
		inUse = true;
		Color c = new Color(0.2F, 0.3F, 0.4F, 0.5F);
		selectButton.targetGraphic.color = c;
		removeButton.gameObject.SetActive(true);
//		selectButton.image = Resources.Load(path);
	}

	public void remove() {
		inUse = false;
		Color c = new Color(1F, 1F, 1F, 1F);
		selectButton.targetGraphic.color = c;
		removeButton.gameObject.SetActive(false);

	}
}
