using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEditor;

public class Character : MonoBehaviour {
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
	public string earStylePath;
	public string irisStylePath;
	public string eyebrowStylePath;
	public string noseStylePath;
	public string mouthStylePath;
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
	private Sprite[] earStyles;
	private Sprite[] eyeStyles;
	private Sprite[] irisStyles;
	private Sprite[] noseStyles;
	private Sprite[] mouthStyles;
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
	private string selectedGlassesName;
	private string selectedHeadwearName;
	private string selectedHairAccessoryName;
	private string selectedTieName;
	private string selectedPiercingName;




	private bool hasLongHair = false;
	private bool hasShortHair = false;

	private bool hasHeadwear = false;
	private bool hasHairAccessory = false;
	private bool hasTie = false;
	private bool hasGlasses = false;
	private bool hasPiercing = false;
	
	public string characterName;
	private string characterAssignment;
	public TextAsset characters;
	private JSONNode json;

	// Use this for initialization
	void Start () {

		json = JSON.Parse(characters.ToString());

		if (characterName == "me") {
			characterAssignment = characterName;
			assign ();
		} 
	}
	
	public void assign(string character = null) {

		if (character != null) {
			characterAssignment = character;
		}
		Debug.Log("My Character Assignment is " + characterAssignment);

		if (PlayerPrefs.HasKey (characterAssignment)) {
			Debug.Log("Has Character Assignment for " + characterAssignment);
			loadCharacter();
			getSprites();

		} else {
			Debug.Log("Generating Character Assignment");
			randomlyGenerate();
		}


	}


	public void randomlyGenerate() {
		//assign true false values
		randomlyPickColors();

		//load resources
		loadStyles();
	
		//get assignments
		selectRandomStyles();

		//set sprites
		setSprites();

		//set paths
		setPaths();

		//save
		saveCharacter();
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


	public void setTone(int amount) {

		tanTone = amount;
		PlayerPrefs.SetInt ("tan", amount);
		Debug.Log ("Setting tan to " + amount);
		//TODO: Need to reload sprite
		/*
		loadStyles ();
		saveCharacter ();
*/
}

	public void saveCharacter() {
		int[] assignedColors = new int[3] {baseSkinTone, tanTone, selectedHairColor};

		string[] assignedSprites = new string[15] {selectedFaceName,selectedEyebrowName,selectedLongHairName, selectedShortHairName, selectedHairLineName,
																selectedEyesName, selectedIrisName, selectedNoseName, selectedEarsName, selectedMouthName,
																selectedGlassesName, selectedHeadwearName, selectedHairAccessoryName, selectedTieName, selectedPiercingName};

		Prefs.PlayerPrefsX.SetIntArray(characterAssignment + "_avatar", assignedColors);
		Prefs.PlayerPrefsX.SetStringArray(characterAssignment + "_avatar_paths", assignedSprites);

		bool[] options = new bool[]{ hasLongHair, hasShortHair, hasGlasses, hasHeadwear, hasHairAccessory, hasTie, hasPiercing};

		Prefs.PlayerPrefsX.SetBoolArray (characterAssignment + "_options", options);
		Debug.Log("Saving Character " + name);
		PlayerPrefs.SetString (characterAssignment, name);
	}

	public void loadCharacter() {
		name = PlayerPrefs.GetString (characterAssignment);
		Debug.Log ("Loading " + characterAssignment + " with name of " + characterName);
		int[] assignedColors = Prefs.PlayerPrefsX.GetIntArray (characterAssignment + "_avatar");
		bool[] options = Prefs.PlayerPrefsX.GetBoolArray (characterAssignment + "_options");
		hasLongHair = options [0];
		hasShortHair = options [1];
		hasGlasses = options[2];
		hasHeadwear = options [3];
		hasHairAccessory = options [4];
		hasTie = options [5];
		hasPiercing = options [6];

		baseSkinTone = assignedColors[0];
		tanTone = assignedColors[1];
		selectedHairColor = assignedColors[2];

		getPaths();


	}

	public void setOptions() {
		hasLongHair = longHair.enabled;
		hasShortHair = shortHair.enabled;
		hasGlasses = glasses.enabled;
		hasHeadwear = headwear.enabled;
		hasHairAccessory = hairAccessory.enabled;
		hasTie = tie.enabled;
		hasPiercing = piercing.enabled;

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

	}

	public void assignCharacter(string type) {
		string [] dates = Prefs.PlayerPrefsX.GetStringArray(type);
		//TODO: Check if there are no more potential characters
		Debug.Log("There are currently " + dates.Length + " characters left");
		if (dates.Length > 0) {
			List<string> list = new List<string> (dates);
			int selectedCharacter = Random.Range (0, list.Count);
			characterAssignment = list [selectedCharacter];
			list.RemoveAt (selectedCharacter);
			Prefs.PlayerPrefsX.SetStringArray (type, list.ToArray ());
			PlayerPrefs.SetString (characterAssignment, characterName);
		} else {
			Debug.Log("No more characters to assign :(");
		}
	}

	public void assignName(string type) {
		int numNames = json["names"]["men"].Count;
		name = json["names"][type][Random.Range(0,numNames)];
//		int numNames = player.json["names"]["men"].Count;
//		name = player.json ["names"] [type] [Random.Range (0, numNames)];
	}


	public Sprite getHairStyle() {
		return backgroundHairStyles [Random.Range (0, backgroundHairStyles.Length)];
	}

	public void randomlyPickColors() {

		if (Random.Range (0, 2) == 1) {
			hasShortHair = true;
		} else {
			hasShortHair = false;
		}

		if (Random.Range (0, 2) == 1) {
			hasLongHair = true;
		} else {
			hasLongHair = false;
		}

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

		if (Random.Range (0, 2) == 1) {
			hasTie = true;
		} else {
			hasTie = false;
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

		selectedHairColor = Random.Range (0, 8);
		baseSkinTone = Random.Range (0, 4);
		tanTone = 0;
	}


	public void loadStyles() {
			//TODO: When skin tone and color exports arrive, place each in a subfolder, store skin tone in playerprefs
			//face and skintone affected assets will ultimately be: Avatar/Face/<basetone>/<shade>/
			//other assets will be: Avatar/iris/<color>
//		faceStylePath = "Avatar/Face/" + baseSkinTone + "/" + tanTone;
//		earStylePath = "Avatar/Ears/" + baseSkinTone + "/" + tanTone;
//		hairLinePath = "Avatar/Hair/" + selectedHairColor + "/" + "/Line";

		faceStyles = Resources.LoadAll<Sprite> (faceStylePath + baseSkinTone + "/" + tanTone);
			hairlineStyles = Resources.LoadAll<Sprite> (hairLineStylePath + selectedHairColor);
			longHairStyles = Resources.LoadAll<Sprite> (longHairStylePath + selectedHairColor);
			shortHairStyles = Resources.LoadAll<Sprite> (shortHairStylePath + selectedHairColor);
			eyebrowStyles = Resources.LoadAll<Sprite> (eyebrowStylePath + selectedHairColor);
			eyeStyles = Resources.LoadAll<Sprite> (eyebrowStylePath);
			irisStyles = Resources.LoadAll<Sprite> (irisStylePath);
			earStyles = Resources.LoadAll<Sprite> (earStylePath + baseSkinTone + "/" + tanTone);
			noseStyles = Resources.LoadAll<Sprite> (noseStylePath);
			mouthStyles = Resources.LoadAll<Sprite> (mouthStylePath);
			glassStyles = Resources.LoadAll<Sprite> (glassStylePath);
			tieStyles = Resources.LoadAll<Sprite> (tieStylePath);
			headwearStyles = Resources.LoadAll<Sprite> (headwearStylePath);
			hairAccessoryStyles = Resources.LoadAll<Sprite> (hairAccessoryStylePath);
			piercingStyles = Resources.LoadAll<Sprite> (piercingStylePath);

	}

	public void selectRandomStyles() {
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
		selectedTie = Random.Range (0, tieStyles.Length);
		selectedHeadwear = Random.Range (0, headwearStyles.Length);
		selectedHairAccessory = Random.Range (0, hairAccessoryStyles.Length);
		selectedGlasses = Random.Range (0, glassStyles.Length);
		selectedPiercing = Random.Range (0, piercingStyles.Length);

	}
	public void getSprites() {
		foregroundHair.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedForegroundHairPath);
		backgroundHair.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedBackgroundHairPath);
		glasses.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedGlassesPath);
		band.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedBandPath);
		ribbon.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedRibbonPath);
		tie.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedTiePath);
		sunglasses.sprite = AssetDatabase.LoadAssetAtPath<Sprite> (selectedSunglassesPath);
		barette.sprite = AssetDatabase.LoadAssetAtPath<Sprite> (selectedBarettePath);
		piercing.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedPiercingPath);
		bow.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedBowPath);
		face.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedFacePath);
		ears.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedEarsPath);
		hairLine.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedHairLinePath);
		brows.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedEyeBrowPath);
		eyes.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedEyesPath);
		iris.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedIrisPath);
		nose.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedNosePath);
		mouth.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(selectedMouthPath);
		
		setEnabledAttributes ();
	}


	public void setSprites() {
		shortHair.sprite = hairStyles [selectedForegroundHair];
		longHair.sprite = backgroundHairStyles[selectedBackgroundHair];
		glasses.sprite = glassStyles[selectedGlasses];
		headwear.sprite = bandStyles[selectedBand];
		hairAccessory.sprite = ribbonStyles[selectedRibbon];
		tie.sprite = tieStyles[selectedTie];
		piercing.sprite = piercingStyles[selectedPiercing];
		face.sprite = faceStyles[selectedFace];
		ears.sprite = earStyles [selectedEars];
		hairLine.sprite = hairlineStyles [selectedHairLine];
		eyebrows.sprite = eyeBrowStyles [selectedEyeBrow];
		eyes.sprite = eyeStyles [selectedEyes];
		iris.sprite = irisStyles [selectedIris];
		nose.sprite = noseStyles [selectedNose];
		mouth.sprite = mouthStyles [selectedMouth];
		
		setEnabledAttributes ();
	}

	public void setEnabledAttributes() {
		shortHair.enabled = hasShortHair;
		longHair.enabled = hasLongHair;		
		glasses.enabled = hasGlasses;
		headwear.enabled = hasHeadwear;
		hairAccessory.enabled = hasHairAccessory;
		tie.enabled = hasTie;
		piercing.enabled = hasPiercing;

	}

	public void setPaths() {

		selectedBarettePath = AssetDatabase.GetAssetPath(barette.sprite);
		selectedBowPath = AssetDatabase.GetAssetPath(bow.sprite);
		selectedBandPath = AssetDatabase.GetAssetPath(band.sprite);
		selectedEarsPath = AssetDatabase.GetAssetPath(ears.sprite);
		selectedEyeBrowPath = AssetDatabase.GetAssetPath(brows.sprite);
		selectedEyesPath = AssetDatabase.GetAssetPath(eyes.sprite);
		selectedFacePath = AssetDatabase.GetAssetPath(face.sprite);
		selectedForegroundHairPath = AssetDatabase.GetAssetPath(foregroundHair.sprite);
		selectedBackgroundHairPath = AssetDatabase.GetAssetPath (backgroundHair.sprite);
		selectedGlassesPath = AssetDatabase.GetAssetPath(glasses.sprite);
		selectedHairLinePath = AssetDatabase.GetAssetPath(hairLine.sprite);
		selectedIrisPath = AssetDatabase.GetAssetPath(iris.sprite);
		selectedMouthPath = AssetDatabase.GetAssetPath(mouth.sprite);
		selectedNosePath = AssetDatabase.GetAssetPath(nose.sprite);
		selectedPiercingPath = AssetDatabase.GetAssetPath(piercing.sprite);
		selectedRibbonPath = AssetDatabase.GetAssetPath(ribbon.sprite);
		selectedSunglassesPath = AssetDatabase.GetAssetPath(sunglasses.sprite);
		selectedTiePath = AssetDatabase.GetAssetPath(tie.sprite);

	}
	

	public void generateAvatar() {
			selectedBackgroundHair = Random.Range (0, backgroundHairStyles.Length);
			selectedFace = Random.Range (0, faceStyles.Length);
			selectedEars = Random.Range (0, earStyles.Length);
			selectedHairLine = Random.Range (0, hairlineStyles.Length);
			selectedEyes = Random.Range (0, eyeStyles.Length);
			selectedForegroundHair = Random.Range (0, hairStyles.Length);
			selectedIris = Random.Range (0, irisStyles.Length);
			selectedNose = Random.Range (0, noseStyles.Length);
			selectedMouth = Random.Range (0, mouthStyles.Length);
			selectedTie = Random.Range (0, tieStyles.Length);
			selectedBand = Random.Range (0, bandStyles.Length);
			selectedRibbon = Random.Range (0, ribbonStyles.Length);
			selectedSunglasses = Random.Range (0, sunglassStyles.Length);
			selectedBow = Random.Range (0, bowStyles.Length);
			selectedGlasses = Random.Range (0, glassStyles.Length);
			selectedBarette = Random.Range (0, baretteStyles.Length);
			selectedPiercing = Random.Range (0, piercingStyles.Length);


			backgroundHair.sprite = backgroundHairStyles[selectedBackgroundHair];
			face.sprite = faceStyles[selectedFace];
			ears.sprite = earStyles[selectedEars];
			hairLine.sprite = hairlineStyles[selectedHairLine];
			eyes.sprite = eyeStyles[selectedEars];
			foregroundHair.sprite = hairStyles[selectedForegroundHair];
			iris.sprite = irisStyles[selectedIris];
			nose.sprite = noseStyles[selectedNose];
			mouth.sprite = mouthStyles[selectedMouth];
			tie.sprite = tieStyles[selectedTie];
			band.sprite = bandStyles[selectedBand];
			ribbon.sprite = ribbonStyles[selectedRibbon];
			sunglasses.sprite = sunglassStyles[selectedSunglasses];
			bow.sprite = bowStyles[selectedBow];
			glasses.sprite = glassStyles[selectedGlasses];
			barette.sprite = baretteStyles[selectedBarette];
			piercing.sprite = piercingStyles[selectedPiercing];
			brows.sprite = eyeBrowStyles[selectedEyeBrow];
	}
	// Update is called once per frame
	void Update () {
	
	}
}
