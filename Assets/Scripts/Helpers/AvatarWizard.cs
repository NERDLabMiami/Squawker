using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarWizard : MonoBehaviour {
	public AvatarColorSelector skinToneColorSelector;
	public AvatarColorSelector hairColorSelector;
	public Text currentFeatureText;
	public Button nextOptionButton;
	public Button previousOptionButton;
	public GameObject saveButton;
	public Character avatar;
	public Player player;

	public ImageSet[] folders;
	public int currentFolderIndex = 0;

	private int categoryIndex;
	public GameObject optionObject;
	public AvatarOptions[] options;

		// Use this for initialization
	void Start () {
		categoryIndex = 0;
		options = optionObject.GetComponentsInChildren<AvatarOptions>(true);
		options[0].gameObject.SetActive(true);
		currentFeatureText.text = options[0].gameObject.name;

	}


	public void saveAndContinue() {
		Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		PlayerBehavior pb = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerBehavior> ();
		//TODO: Set Character List Through Gender Preference
		Debug.Log("Resetting Stats");
		string player_id = PlayerPrefs.GetString("qualtrics_id");
		player.resetStats();
		PlayerPrefs.SetString("qualtrics_id", player_id);

		PlayerPrefs.SetInt("game in progress", 1);

//		avatar.setColors(skinToneColorSelector.selectedColor, 0, hairColorSelector.selectedColor);
		avatar.setPaths();

		avatar.saveCharacter(true);
		//TODO: Hair Color & Style
		pb.newPlayer (avatar.getSkinTone (), avatar.getHairColor(), avatar.face.sprite.name, avatar.ears.sprite.name, avatar.eyes.sprite.name, avatar.iris.sprite.name, avatar.mouth.sprite.name, avatar.nose.sprite.name);
		player.loadSceneNumber (1);
	}


	public void nextCategory() {
		previousOptionButton.gameObject.SetActive(true);
		options[categoryIndex].gameObject.SetActive(false);

		categoryIndex++;
		if (categoryIndex == options.Length -1) {
			saveButton.GetComponent<Button>().interactable = true;
			nextOptionButton.gameObject.SetActive(false);
		}

		if (categoryIndex >= options.Length) {
			categoryIndex = 0;
		}

		currentFeatureText.text = options[categoryIndex].gameObject.name;

		options[categoryIndex].gameObject.SetActive(true);



	}

	public void previousCategory() {
		options[categoryIndex].gameObject.SetActive(false);
		nextOptionButton.gameObject.SetActive(true);

		categoryIndex--;
		if (categoryIndex < 0) {
			categoryIndex = options.Length - 1;

		}

		if (categoryIndex == 0) {
			previousOptionButton.gameObject.SetActive(false);
		}

		currentFeatureText.text = options[categoryIndex].gameObject.name;

		options[categoryIndex].gameObject.SetActive(true);


	}
		
}
