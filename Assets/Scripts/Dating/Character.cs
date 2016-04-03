using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class Character : MonoBehaviour {

	public Color hair;
	public Color skin;

	//HAIR
	public Image longHair;
	public Image shortHair;
	public Image hairLine;
	public Image eyebrows;

	//FACE
	public Image face;
	public Image ears;
	public Image eyes;
	public Image iris;
	public Image nose;
	public Image mouth;
	public Image faceTan;
	public Image earsTan;
	public Image mole;

	//ACCESSORIES
	public Image glasses;
	public Image tie;
	public Image headwear;
	public Image hairAccessory;
	public Image piercing;

	public string longHairStylePath;
	public string shortHairStylePath;
	public string hairLineStylePath;
	public string faceStylePath;
	public string faceTanLevelPath;
	public string earsTanLevelPath;
	public string earStylePath;
	public string irisStylePath;
	public string eyeStylePath;
	public string eyebrowStylePath;
	public string noseStylePath;
	public string mouthStylePath;
	public string moleStylePath;
	public string glassStylePath;
	public string tieStylePath;
	public string headwearStylePath;
	public string hairAccessoryStylePath;
	public string piercingStylePath;

	//Entire Folders of Sprites
	private Sprite[] longHairStyles;
	private Sprite[] shortHairStyles;
	private Sprite[] hairlineStyles;
	private Sprite[] faceStyles;
	private Sprite[] faceTanLevels;
	private Sprite[] earStyles;
	private Sprite[] earTanLevels;
	private Sprite[] eyeStyles;
	private Sprite[] irisStyles;
	private Sprite[] noseStyles;
	private Sprite[] mouthStyles;
	private Sprite[] moleStyles;
	private Sprite[] eyebrowStyles;

	//ACCESSORIES
	private Sprite[] glassStyles;
	private Sprite[] tieStyles;
	private Sprite[] headwearStyles;
	private Sprite[] hairAccessoryStyles;
	private Sprite[] piercingStyles;


	private int baseSkinTone;
	private int tanTone;
	private int selectedHairColor;

	//used for random generation

	private int selectedLongHair;
	private int selectedShortHair;
	private int selectedHairLine;
	private int selectedEyebrow;

	private int selectedFace;
	private int selectedEars;
	private int selectedEyes;
	private int selectedIris;
	private int selectedNose;
	private int selectedMouth;
	private int selectedMole;

	private int selectedGlasses;
	private int selectedTie;
	private int selectedHeadwear;
	private int selectedHairAccessory;
	private int selectedPiercing;
	
	//name of resource selected
	private string selectedLongHairName;
	private string selectedShortHairName;
	private string selectedHairLineName;
	private string selectedEyebrowName;

	private string selectedFaceName;
	private string selectedEarsName;
	private string selectedEyesName;
	private string selectedIrisName;
	private string selectedNoseName;
	private string selectedMouthName;
	private string selectedMoleName;

	private string selectedGlassesName;
	private string selectedHeadwearName;
	private string selectedHairAccessoryName;
	private string selectedTieName;
	private string selectedPiercingName;




	private bool hasLongHair = false;
	private bool hasShortHair = false;
	private bool hasHairLine = false;
	private bool hasEyeBrows = true;
	private bool hasTan = false;

	private bool hasHeadwear = false;
	private bool hasHairAccessory = false;
	private bool hasTie = false;
	private bool hasMole = false;

	private bool hasGlasses = false;
	private bool hasPiercing = false;
	
	public string characterName;
	public string characterAssignment;
	public string characterType;
	public TextAsset characters;
	private JSONNode json;

	public Color[] hairCombinations;
	public Color[] skinCombinations;
	public Color[] tans;

	// Use this for initialization
	void Start () {
		json = JSON.Parse(characters.ToString());
		if (characterName == "me") {
			characterAssignment = characterName;
			assign ();
		}
		else if(characterName == "new") {
			//skipping assignment
			characterAssignment = "me";
			PlayerPrefs.SetInt("tan",0);

		}
	}

	public bool hasCompleteCharacter() {
		if (face.gameObject.activeSelf && ears.gameObject.activeSelf && nose.gameObject.activeSelf && mouth.gameObject.activeSelf && iris.gameObject.activeSelf) {
			return true;
		}
		else {
			return false;
		}
	}

	public void setHairColor() {
		eyebrows.color = hair;
		hairLine.color = hair;
		shortHair.color = hair;
		longHair.color = hair;
	}

	public void setBaseSkinTone() {
		face.color = skin;
		ears.color = skin;
	}

	public void setColors(int skinColor, int tanLevel, int hairColor) {
		selectedHairColor = hairColor;
//		tanTone = tanLevel;
		baseSkinTone = skinColor;
	}

	public string getSkinTone() {
		return skin.ToString ();
	}
	public string getHairColor() {
		return hair.ToString ();
	}
	public void assign(string character = null) {
		Debug.Log ("Character assignment: " + character);
		if (character != null) {
			characterAssignment = character;
		}
		if (PlayerPrefs.HasKey (characterAssignment)) {
			Debug.Log("Has Character Assignment for " + characterAssignment);
			loadCharacter();
			getSprites();

		} else {
			if (characterAssignment == "me") {
				randomlyGenerate(true);
			}
			else {
				randomlyGenerate(false);
			}
		}


	}


	public void randomlyGenerate(bool save = false) {
		//assign true false values
		randomlyPickColors();

		//load resources
		loadStyles();
	

		//assign sprites
		if (characterAssignment == "new" || characterAssignment == "me") {
			//get assignments
			selectRandomStyles(false);
			generateAvatar(false);
			//set sprites
			setSprites();
			glasses.enabled = false;
			hairAccessory.enabled = false;
			tie.enabled = false;
			piercing.enabled = false;
			headwear.enabled = false;
		}
		else {
			//get assignments
			selectRandomStyles(true);
			generateAvatar (true);
			//set sprites
			setSprites();
		}

		//set paths
		setPaths();

		//save
		if (save) {
			saveCharacter();
		}
	}
	
	public string getCharacterAssignment() {
		return characterAssignment;
	}

	public bool wearingTie() {
		return hasTie;
	}

	public bool wearingHeadwear() {
		return hasHeadwear;
	}

	public bool wearingHairAccessory() {
		return hasHairAccessory;
	}

	public bool wearingGlasses() {
		return hasGlasses;
	}
	

	public bool wearingPiercing() {
		return hasPiercing;
	}
		
	public int setTone(int amount) {

		if (amount <= 0) {
			amount = 0;
		}

		tanTone = amount;
		PlayerPrefs.SetInt ("tan", tanTone);
		if (amount == 0) {
			faceTan.CrossFadeColor(tans[tanTone], 1f, true, true);
			earsTan.CrossFadeColor(tans[tanTone], 1f, true, true);
			faceTan.enabled = true;
			earsTan.enabled = true;
			hasTan = true;
		}
		else {
			faceTan.CrossFadeColor(tans[tanTone], 1f, true, true);
			earsTan.CrossFadeColor(tans[tanTone], 1f, true, true);
			faceTan.enabled = true;
			earsTan.enabled = true;
			hasTan = false;			
		}
		saveCharacter ();
		return tanTone;
}

	public void updateCharacter() {
		setPaths ();
		setOptions ();
		saveCharacter ();
		Debug.Log("FINISHED SAVING CHARACTER: " + characterAssignment);
		/*
		setPaths();
		saveCharacter(false);
*/
}

	public void reloadCharacter() {
		Debug.Log("STARTED RELOADING CHARACTER");
		if (PlayerPrefs.HasKey (characterAssignment)) {
			Debug.Log("Has Character Assignment for " + characterAssignment);
			loadCharacter();
			getSprites();
		} else {
			Debug.Log("Character assignment missing");
		}
		Debug.Log("FINISHED RELOADING CHARACTER");
	}

	public void saveCharacter(bool disableAccessories = false) {

		float[] assignedHair = new float[4] {hair.r, hair.g, hair.b, hair.a};
		float[] assignedSkin = new float[4] {skin.r, skin.g, skin.b, skin.a};

		string[] assignedSprites = new string[18] {selectedFaceName,selectedEyebrowName,selectedLongHairName, selectedShortHairName, selectedHairLineName,
																selectedEyesName, selectedIrisName, selectedNoseName, selectedEarsName, selectedMouthName,
																selectedGlassesName, selectedHeadwearName, selectedHairAccessoryName, selectedTieName, selectedPiercingName,
																selectedFaceName, selectedEarsName, selectedMoleName};
		json = JSON.Parse(characters.ToString());
//		JSONNode character = json [characterAssignment];
//		Debug.Log ("NUMBER IN JSON: " + json.Count);

		Debug.Log ("NUMBER IN CHARACTER " + characterAssignment + ":"+ json[characterAssignment]["character_type"].Count);
		characterType = json[characterAssignment]["character_type"];

		PlayerPrefs.SetString (characterAssignment + "_type", characterType);
		Debug.Log ("Character: " + characterAssignment + " Character Type: " + characterType);
		PlayerPrefs.SetString (characterAssignment + "_type", "C22");
		Debug.Log("Assigned Sprites: " + assignedSprites);
		Prefs.PlayerPrefsX.SetStringArray(characterAssignment + "_avatar_paths", assignedSprites);
		//added 12.8.15, for avatar creation
		setOptions(disableAccessories);
		bool[] options = new bool[]{ hasLongHair, hasShortHair, hasGlasses, hasHeadwear, hasHairAccessory, hasTie, hasPiercing, hasMole, hasHairLine, hasEyeBrows, hasTan};

		Prefs.PlayerPrefsX.SetBoolArray (characterAssignment + "_options", options);
		Prefs.PlayerPrefsX.SetFloatArray(characterAssignment + "_hair", assignedHair);
		Prefs.PlayerPrefsX.SetFloatArray(characterAssignment + "_skin", assignedSkin);

		PlayerPrefs.SetString (characterAssignment, characterName);
	}

	public void loadCharacter() {
		name = PlayerPrefs.GetString (characterAssignment);
		Debug.Log ("Loading " + characterAssignment + " with name of " + name);
//		int[] assignedColors = Prefs.PlayerPrefsX.GetIntArray (characterAssignment + "_avatar");
		float[] skinColors = Prefs.PlayerPrefsX.GetFloatArray(characterAssignment + "_skin");
		float[] hairColors = Prefs.PlayerPrefsX.GetFloatArray(characterAssignment + "_hair");
		bool[] options = Prefs.PlayerPrefsX.GetBoolArray (characterAssignment + "_options");
		characterType = PlayerPrefs.GetString (characterAssignment + "_type");

		hasLongHair = options [0];
		hasShortHair = options [1];
		hasGlasses = options[2];
		hasHeadwear = options [3];
		hasHairAccessory = options [4];
		hasTie = options [5];
		hasPiercing = options [6];
		hasMole = options[7];
		hasHairLine = options[8];
		hasEyeBrows = options[9];
		hasTan = options[10];

		tanTone = PlayerPrefs.GetInt("tan", 0);
		//TODO: Check for null values
		hair = new Color(hairColors[0], hairColors[1], hairColors[2], hairColors[3]);
		skin = new Color(skinColors[0], skinColors[1], skinColors[2], skinColors[3]);
		setBaseSkinTone();
		setHairColor();
		getPaths();


	}

	public void getMole(int visit) {
		moleStyles = Resources.LoadAll<Sprite>(moleStylePath);
		if (mole.enabled) {
			for (int i = 0; i < moleStyles.Length; i++) {
				if (selectedMoleName == moleStyles[i].name) {
					selectedMole = i;
				}
			}
			if (selectedMole < moleStyles.Length - 1) {
				selectedMole++;
			}
		}
		else {
			selectedMole = 0;		
		}

		mole.sprite = moleStyles[selectedMole];
		selectedMoleName = mole.sprite.name;
		mole.gameObject.SetActive (true);
		mole.enabled = true;
		saveCharacter ();
	}

	public void removeMole() {
		moleStyles = Resources.LoadAll<Sprite>(moleStylePath);
		int moleNumber = 0;
		for (int i = 0; i < moleStyles.Length; i++) {
			if (selectedMoleName == moleStyles[i].name) {
				moleNumber = i;
			}
		}
		moleNumber--;
		if (moleNumber <= 0) {
			mole.sprite.name = "none";
			mole.gameObject.SetActive (false);
			mole.enabled = false;
		}
		else {
			mole.sprite.name = moleStyles[moleNumber].name;
			selectedMoleName = mole.sprite.name;
		}
			saveCharacter ();
	}

	public void setOptions(bool noAccessories = false) {
		if (!longHair.gameObject.activeSelf) {
			longHair.enabled = false;
		}
		if (!shortHair.gameObject.activeSelf) {
			shortHair.enabled = false;
		}

		if (!hairLine.gameObject.activeSelf) {
			hairLine.enabled = false;
		}
		if (noAccessories) {
			glasses.enabled = false;
			headwear.enabled = false;
			hairAccessory.enabled = false;
			tie.enabled = false;
			piercing.enabled = false;
			mole.enabled = false;
		}
		else {
			if (!glasses.gameObject.activeSelf) {
				glasses.enabled = false;
			}
			if (!headwear.gameObject.activeSelf) {
				headwear.enabled = false;
			}
			if (!hairAccessory.gameObject.activeSelf) {
				hairAccessory.enabled = false;
			}

			if (!tie.gameObject.activeSelf) {
				tie.enabled = false;
			}

			if (!piercing.gameObject.activeSelf) {
				piercing.enabled = false;
			}

			if(!mole.gameObject.activeSelf) {
				mole.enabled = false;
			}

		}
		hasLongHair = longHair.enabled;
		hasShortHair = shortHair.enabled;
		hasGlasses = glasses.enabled;
		hasHeadwear = headwear.enabled;
		hasHairAccessory = hairAccessory.enabled;
		hasTie = tie.enabled;
		hasPiercing = piercing.enabled;
		hasMole = mole.enabled;
		hasHairLine = hairLine.enabled;
		hasEyeBrows = true;
	

	}
	public void getPaths() {
		string[] assignedSprites = Prefs.PlayerPrefsX.GetStringArray(characterAssignment + "_avatar_paths");

		selectedFaceName = assignedSprites [0];
		selectedEyebrowName = assignedSprites [1];
		selectedLongHairName = assignedSprites [2];
		selectedShortHairName = assignedSprites [3];
		selectedHairLineName = assignedSprites [4];

		selectedEyesName = assignedSprites [5];
		selectedIrisName = assignedSprites [6];
		selectedNoseName = assignedSprites [7];
		selectedEarsName = assignedSprites[8];
		selectedMouthName = assignedSprites[9];

		selectedGlassesName = assignedSprites[10];
		selectedHeadwearName = assignedSprites[11];
		selectedHairAccessoryName = assignedSprites[12];
		selectedTieName = assignedSprites[13];
		selectedPiercingName = assignedSprites[14];

		selectedMoleName = assignedSprites[17];
	}

	public int assignCharacter(string type) {
		string [] dates = Prefs.PlayerPrefsX.GetStringArray(type);
		//TODO: Check if there are no more potential characters
		if (dates.Length > 0) {
			List<string> list = new List<string> (dates);
			int selectedCharacter = Random.Range (0, list.Count);
			characterAssignment = list [selectedCharacter];
			Debug.Log("Random Character Selection: "  + selectedCharacter);
			list.RemoveAt (selectedCharacter);
			Prefs.PlayerPrefsX.SetStringArray (type, list.ToArray ());
			PlayerPrefs.SetString (characterAssignment, characterName);
			Debug.Log ("Setting Character Type " + characterAssignment + " for " + characterName);
		} else {
			Debug.Log("No more characters to assign :(");
		}
		return dates.Length;
	}

	public void assignName(string gender) {
		int numNames = json["names"][gender].Count;
		name = json["names"][gender][Random.Range(0,numNames)];
		characterName = name;
//		int numNames = player.json["names"]["men"].Count;
//		name = player.json ["names"] [type] [Random.Range (0, numNames)];
	}


	public Sprite getHairStyle() {
		return longHairStyles [Random.Range (0, longHairStyles.Length)];
	}

	private void chooseHeadwear() {
		if (Random.Range (0, 2) == 1) {
			hasHeadwear = true;
		} else {
			hasHeadwear = false;
		}

		if (Random.Range (0, 2) == 1) {
			hasHairAccessory = true;
		} else {
			hasHairAccessory = false;
		}

	}

	private void chooseTie() {
		if (Random.Range (0, 2) == 1) {
			hasTie = true;
		} else {
			hasTie = false;
		}
	}

	public void randomlyPickColors() {
		hasMole = false;

		string gender = PlayerPrefs.GetString("gender preference","both");

		if (Random.Range (0, 10) > 5) {
			hasShortHair = true;
			hasLongHair = false;
		} else {
			hasShortHair = false;
			hasLongHair = true;
		}

		switch(gender) {
			case "men":
				chooseTie();
				hasHeadwear = false;
				hasHairAccessory = false;
				hasLongHair = false;
				break;
			case "women":
				chooseHeadwear();
				hasTie = false;
				break;
			case "both":
				chooseTie();
				chooseHeadwear();
				break;
		}

		if (Random.Range (0, 2) == 1) {
			hasGlasses = true;
		} else {
			hasGlasses = false;
		}

		if (Random.Range (0, 2) == 1) {
			hasPiercing = true;
		} else {
			hasPiercing = false;
		}
	

//		if (characterAssignment.Length == 0) {
			selectedHairColor = Random.Range (0, hairCombinations.Length);
			baseSkinTone = Random.Range (0, skinCombinations.Length);
			//TODO: Check for no colors
			if (hairCombinations.Length > 0) {
				hair = hairCombinations[selectedHairColor];
			}
			if (skinCombinations.Length > 0) {
				skin = skinCombinations[baseSkinTone];
			}

			setHairColor();
			setBaseSkinTone();
//		}
//		else {
//			Debug.Log("Not assigning colors for " + characterAssignment);
//		}

		tanTone = 0;
	}


	public void loadStyles() {

			faceStyles = Resources.LoadAll<Sprite> (faceStylePath);
			hairlineStyles = Resources.LoadAll<Sprite> (hairLineStylePath);
			longHairStyles = Resources.LoadAll<Sprite> (longHairStylePath);
			shortHairStyles = Resources.LoadAll<Sprite> (shortHairStylePath);
			eyebrowStyles = Resources.LoadAll<Sprite> (eyebrowStylePath);
			eyeStyles = Resources.LoadAll<Sprite> (eyeStylePath);
			irisStyles = Resources.LoadAll<Sprite> (irisStylePath);
			earStyles = Resources.LoadAll<Sprite> (earStylePath);

			moleStyles = Resources.LoadAll<Sprite>(moleStylePath);
			noseStyles = Resources.LoadAll<Sprite> (noseStylePath);
			mouthStyles = Resources.LoadAll<Sprite> (mouthStylePath);
			glassStyles = Resources.LoadAll<Sprite> (glassStylePath);
			tieStyles = Resources.LoadAll<Sprite> (tieStylePath);
			headwearStyles = Resources.LoadAll<Sprite> (headwearStylePath);
			hairAccessoryStyles = Resources.LoadAll<Sprite> (hairAccessoryStylePath);
			piercingStyles = Resources.LoadAll<Sprite> (piercingStylePath);

	}

	public void selectRandomStyles(bool accessories = true) {
		selectedLongHair = Random.Range (0, longHairStyles.Length);
		selectedFace = Random.Range (0, faceStyles.Length);
		selectedEars = Random.Range (0, earStyles.Length);
		selectedHairLine = Random.Range (0, hairlineStyles.Length);
		selectedEyes = Random.Range (0, eyeStyles.Length);
		selectedEyebrow = Random.Range (0, eyebrowStyles.Length);
		selectedShortHair = Random.Range (0, shortHairStyles.Length);
		selectedIris = Random.Range (0, irisStyles.Length);
		selectedNose = Random.Range (0, noseStyles.Length);
		selectedMouth = Random.Range (0, mouthStyles.Length);
		if (accessories) {
			selectedTie = Random.Range (0, tieStyles.Length);
			selectedHeadwear = Random.Range (0, headwearStyles.Length);
			selectedHairAccessory = Random.Range (0, hairAccessoryStyles.Length);
			selectedGlasses = Random.Range (0, glassStyles.Length);
			selectedPiercing = Random.Range (0, piercingStyles.Length);
		}
		selectedMole = Random.Range(0, moleStyles.Length);

	}
	public void getSprites() {
		hairLine.sprite = Resources.Load<Sprite> (hairLineStylePath + "/"  + selectedHairLineName);
		shortHair.sprite = Resources.Load<Sprite>(shortHairStylePath + "/"  + selectedShortHairName);
		longHair.sprite = Resources.Load<Sprite>(longHairStylePath + "/" + selectedLongHairName);
		eyebrows.sprite = Resources.Load<Sprite>(eyebrowStylePath + "/" + selectedEyebrowName);

		face.sprite = Resources.Load<Sprite> (faceStylePath + "/" + selectedFaceName);	
		ears.sprite = Resources.Load<Sprite> (earStylePath + "/" + selectedEarsName);

		mouth.sprite = Resources.Load<Sprite> (mouthStylePath + "/" + selectedMouthName);
		nose.sprite = Resources.Load<Sprite> (noseStylePath + "/" + selectedNoseName);
		eyes.sprite = Resources.Load<Sprite> (eyeStylePath + "/" + selectedEyesName);
		iris.sprite = Resources.Load<Sprite> (irisStylePath + "/" + selectedIrisName);
		mole.sprite = Resources.Load<Sprite> (moleStylePath + "/" + selectedMoleName);
		glasses.sprite = Resources.Load<Sprite> (glassStylePath + "/" + selectedGlassesName);
		headwear.sprite = Resources.Load<Sprite> (headwearStylePath + "/" + selectedHeadwearName);
		hairAccessory.sprite = Resources.Load<Sprite> (hairAccessoryStylePath + "/" + selectedHairAccessoryName);
		tie.sprite = Resources.Load<Sprite> (tieStylePath + "/" + selectedTieName);
		piercing.sprite = Resources.Load<Sprite> (piercingStylePath + "/" + selectedPiercingName);
		faceTan.sprite = Resources.Load<Sprite> (faceStylePath + "/" + selectedFaceName);	
		earsTan.sprite = Resources.Load<Sprite> (earStylePath + "/"  + selectedEarsName);

		if (tanTone >= 1 && tans.Length >= tanTone) {
			faceTan.color = tans[tanTone];
			earsTan.color = tans[tanTone];
		}

		setEnabledAttributes ();
	}


	public void setSprites() {
		shortHair.sprite = shortHairStyles [selectedShortHair];
		longHair.sprite = longHairStyles[selectedLongHair];
		face.sprite = faceStyles[selectedFace];
		ears.sprite = earStyles [selectedEars];
		hairLine.sprite = hairlineStyles [selectedHairLine];
		eyebrows.sprite = eyebrowStyles[selectedEyebrow];
		eyes.sprite = eyeStyles [selectedEyes];
		iris.sprite = irisStyles [selectedIris];
		nose.sprite = noseStyles [selectedNose];
		mouth.sprite = mouthStyles [selectedMouth];
		mole.sprite = moleStyles[selectedMole];
		faceTan.sprite = face.sprite;
		earsTan.sprite = ears.sprite;
		if (characterAssignment != "me") {
			glasses.sprite = glassStyles[selectedGlasses];
			headwear.sprite = headwearStyles[selectedHeadwear];
			hairAccessory.sprite = hairAccessoryStyles[selectedHairAccessory];
			tie.sprite = tieStyles[selectedTie];
			piercing.sprite = piercingStyles[selectedPiercing];
		}

		setEnabledAttributes ();

	}

	public void setEnabledAttributes() {
		shortHair.enabled = hasShortHair;
		longHair.enabled = hasLongHair;		
		hairLine.enabled = hasHairLine;
		eyebrows.enabled = hasEyeBrows;

//		if (characterAssignment != "me") {
			glasses.enabled = hasGlasses;
			headwear.enabled = hasHeadwear;
			hairAccessory.enabled = hasHairAccessory;
			tie.enabled = hasTie;
			piercing.enabled = hasPiercing;
			mole.enabled = hasMole;
//		}
	}

	public void setPaths() {
		if (shortHair.sprite) {
			selectedShortHairName = shortHair.sprite.name;
		}
		else {
			selectedShortHairName = "none";
		}
		if (longHair.sprite) {
			selectedLongHairName = longHair.sprite.name;
		}
		else {
			selectedLongHairName = "none";
		}
		if (hairLine.sprite) {
			selectedHairLineName = hairLine.sprite.name;
		}
		else {
			selectedHairLineName = "none";
		}

		if (hairAccessory.sprite) {
			selectedHairAccessoryName = hairAccessory.sprite.name;
		}
		else {
			selectedHairAccessoryName = "none";
		}

		if (headwear.sprite) {
			selectedHeadwearName = headwear.sprite.name;
		}
		else {
			selectedHeadwearName = "none";
		}

		if (eyebrows.sprite) {
			selectedEyebrowName = eyebrows.sprite.name;
		}
		else {
			selectedEyebrowName = "none";
		}

		if (mole.sprite) {
			selectedMoleName = mole.sprite.name;
		}
		else {
			selectedMoleName = "none";
		}

		if (glasses.sprite) {
			selectedGlassesName = glasses.sprite.name;
		}
		else {
			selectedGlassesName = "none";
		}

		if (tie.sprite) {
			selectedTieName = tie.sprite.name;
		}
		else {
			selectedTieName = "none";
		}

		if (piercing.sprite) {
			selectedPiercingName = piercing.sprite.name;
		}
		else {
			selectedPiercingName = "none";
		}
		if (ears.sprite) {
			selectedEarsName = ears.sprite.name;
		}
		if (eyes.sprite) {
			selectedEyesName = eyes.sprite.name;
		}
		if (iris.sprite) {
			selectedIrisName = iris.sprite.name;
		}
		if (face.sprite) {
			selectedFaceName = face.sprite.name;
		}
		if (mouth.sprite) {
			selectedMouthName = mouth.sprite.name;
		}

		if (nose.sprite) {
			selectedNoseName = nose.sprite.name;
		}

	}
	

	public void generateAvatar(bool accessories = true) {
			selectedLongHair = Random.Range (0, longHairStyles.Length);
			selectedFace = Random.Range (0, faceStyles.Length);
			selectedEars = Random.Range (0, earStyles.Length);
			selectedHairLine = Random.Range (0, hairlineStyles.Length);
			selectedEyes = Random.Range (0, eyeStyles.Length);
			selectedShortHair = Random.Range (0, shortHairStyles.Length);
			selectedIris = Random.Range (0, irisStyles.Length);
			selectedNose = Random.Range (0, noseStyles.Length);
			selectedMouth = Random.Range (0, mouthStyles.Length);
			selectedTie = Random.Range (0, tieStyles.Length);
			longHair.sprite = longHairStyles [selectedLongHair];
			face.sprite = faceStyles[selectedFace];
			ears.sprite = earStyles[selectedEars];
			faceTan.sprite = faceStyles[selectedFace];
			earsTan.sprite = earStyles[selectedEars];

			hairLine.sprite = hairlineStyles[selectedHairLine];
			hasHairLine = true;
			eyes.sprite = eyeStyles[selectedEars];
			shortHair.sprite = shortHairStyles [selectedShortHair];
			iris.sprite = irisStyles[selectedIris];
			nose.sprite = noseStyles[selectedNose];
			mouth.sprite = mouthStyles[selectedMouth];
			mole.sprite = moleStyles[selectedMole];

			eyebrows.sprite = eyebrowStyles [selectedEyebrow];

		if (accessories) {
			selectedHeadwear = Random.Range (0, headwearStyles.Length);
			selectedHairAccessory = Random.Range (0, hairAccessoryStyles.Length);
			selectedGlasses = Random.Range (0, glassStyles.Length);
			selectedPiercing = Random.Range (0, piercingStyles.Length);
			selectedEyebrow = Random.Range (0, eyebrowStyles.Length);
			selectedMole = Random.Range (0, moleStyles.Length);
			tie.sprite = tieStyles[selectedTie];
			headwear.sprite = headwearStyles [selectedHeadwear];
			hairAccessory.sprite = hairAccessoryStyles [selectedHairAccessory];
			glasses.sprite = glassStyles[selectedGlasses];
			piercing.sprite = piercingStyles[selectedPiercing];
		}
		else {
			/*
			hairLine.gameObject.SetActive(hasHairLine);
			shortHair.gameObject.SetActive(hasShortHair);
			longHair.gameObject.SetActive(hasLongHair);
*/
			nose.gameObject.SetActive(true);
			mouth.gameObject.SetActive(true);
			iris.gameObject.SetActive(true);
			eyes.gameObject.SetActive(true);
			face.gameObject.SetActive(true);
			faceTan.gameObject.SetActive (true);
			earsTan.gameObject.SetActive (true);
			ears.gameObject.SetActive(true);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
