using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarWizard : MonoBehaviour {
	public int skinTone;
	public int hairColor;
	public Text previousFeatureText;
	public Text nextFeatureText;
	public Text currentFeatureText;
	public Button nextOptionButton;
	public Button previousOptionButton;
	public ImageSet[] folders;
	public int currentFolderIndex = 0;

		// Use this for initialization
	void Start () {
		currentFolderIndex = 0;
		currentFeatureText.text = folders[currentFolderIndex].description;
		populateOptions();
		folders[currentFolderIndex].set ();
		folders[currentFolderIndex].checkForColor();
	}

	
	// Update is called once per frame
	void Update () {
	
	}
	void populateOptions() {
		nextOptionButton.image.sprite = folders[currentFolderIndex].getNextSprite();
		previousOptionButton.image.sprite = folders[currentFolderIndex].getPreviousSprite();
	}

	void populateNavigation() {
		if (currentFolderIndex == folders.Length - 1) {
			nextFeatureText.text = folders[0].description;
		}
		else {
			nextFeatureText.text = folders[currentFolderIndex+1].description;
			//			previousFeatureButton.GetComponent<Text>().text = folders[currentFolderIndex-1].description;
		}

		if (currentFolderIndex == 0) {
			previousFeatureText.text = folders[folders.Length - 1].description;
		}
		else {
			previousFeatureText.text = folders[currentFolderIndex-1].description;
			//			previousFeatureButton.GetComponent<Text>().text = folders[currentFolderIndex-1].description;
		}

		currentFeatureText.text = folders[currentFolderIndex].description;

	}

	private void loadOptions() {

	}

	public void nextFeature() {
		if (folders[currentFolderIndex].colorSelector) {
			folders[currentFolderIndex].colorSelector.gameObject.SetActive(false);

		}
		currentFolderIndex++;
		if (currentFolderIndex >= folders.Length) {
			currentFolderIndex = 0;
		}

		folders[currentFolderIndex].checkForColor();
		if (folders[currentFolderIndex].startWithSpriteEnabled) {
			folders[currentFolderIndex].set ();
		}
		populateNavigation();
		populateOptions();

	}

	public void previousFeature() {
		if (folders[currentFolderIndex].colorSelector) {
			folders[currentFolderIndex].colorSelector.gameObject.SetActive(false);
		}

		currentFolderIndex--;
		if (currentFolderIndex < 0) {
			currentFolderIndex = folders.Length - 1;
		}
		folders[currentFolderIndex].checkForColor();
		if (folders[currentFolderIndex].startWithSpriteEnabled) {
			folders[currentFolderIndex].set ();
		}
		populateNavigation();
		populateOptions();

	}

	public void nextOption() {
		folders[currentFolderIndex].next ();
		populateOptions();

	}

	public void previousOption() {
		folders[currentFolderIndex].previous();
		populateOptions();
	}
}
