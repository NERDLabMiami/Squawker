using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class Character : MonoBehaviour {

	public Image backgroundHair;
	public Image face;
	public Image ears;
	public Image hairLine;
	public Image eyes;
	public Image foregroundHair;
	public Image iris;
	public Image nose;
	public Image mouth;
	public Image brows;

	public Image glasses;
	public Image sunglasses;
	public Image tie;
	public Image band;
	public Image ribbon;
	public Image barette;
	public Image bow;
	public Image piercing;


	private Sprite[] backgroundHairStyles;
	private Sprite[] faceStyles;
	private Sprite[] earStyles;
	private Sprite[] hairlineStyles;
	private Sprite[] eyeStyles;
	private Sprite[] hairStyles;
	private Sprite[] irisStyles;
	private Sprite[] noseStyles;
	private Sprite[] mouthStyles;
	private Sprite[] eyeBrowStyles;
	private Sprite[] sunglassStyles;
	private Sprite[] glassStyles;
	private Sprite[] tieStyles;
	private Sprite[] ribbonStyles;
	private Sprite[] bandStyles;
	private Sprite[] baretteStyles;
	private Sprite[] bowStyles;
	private Sprite[] piercingStyles;



	private int selectedBackgroundHair;
	private int selectedFace;
	private int selectedEars;
	private int selectedHairLine;
	private int selectedEyes;
	private int selectedEyeBrow;
	private int selectedForegroundHair;
	private int selectedIris;
	private int selectedNose;
	private int selectedMouth;
	private int selectedHairColor;
	private int baseSkinTone;
	private int selectedSunglasses;
	private int selectedGlasses;
	private int selectedBow;
	private int selectedBarette;
	private int selectedPiercing;
	private int selectedTie;
	private int selectedBand;
	private int selectedRibbon;


	private int tanTone;

	private bool hasLongHair = false;
	private bool hasShortHair = false;

	private bool hasBand = false;
	private bool hasRibbon = false;
	private bool hasTie = false;
	private bool hasGlasses = false;

	private bool hasSunglasses = false;
	private bool hasBow = false;
	private bool hasBarette = false;
	private bool hasPiercing = false;



	public string name;
	private string characterAssignment;
	public TextAsset characters;
	private JSONNode json;
//	private Player player;
	
//	private Image 
	// Use this for initialization
	void Start () {
//		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		json = JSON.Parse(characters.ToString());

		if (name == "me") {
			characterAssignment = name;
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
			loadStyles ();
			assignStyles ();
			
		} else {
			Debug.Log("Generating Character Assignment");
			
			randomlyGenerate();
		}
		saveCharacter ();
	}

	public string getCharacterAssignment() {
		return characterAssignment;
	}

	public bool wearingTie() {
		return hasTie;
	}

	public bool wearingBand() {
		return hasBand;
	}

	public bool wearingRibbon() {
		return hasRibbon;
	}
	

	public bool wearingGlasses() {
		return hasGlasses;
	}

	public bool wearingSunglasses() {
		return hasSunglasses;
	}

	public bool wearingBarette() {
		return hasBarette;
	}


	public bool wearingBow() {
		return hasBow;
	}

	public bool wearingPiercing() {
		return hasPiercing;
	}

	public void randomlyGenerate() {
		randomlyPickColors ();
		loadStyles ();
		generateAvatar ();
		assignStyles ();

	}
	public void setTone(int amount) {
		tanTone = amount;
		PlayerPrefs.SetInt ("tan", amount);
		Debug.Log ("Setting tan to " + amount);
		loadStyles ();
		assignStyles ();
		saveCharacter ();
	}

	public void saveCharacter() {
		int[] prefs = new int[21] {baseSkinTone, tanTone, selectedFace,selectedHairColor,selectedEyeBrow,selectedBackgroundHair, selectedForegroundHair, selectedHairLine, selectedEyes, selectedIris,
			selectedNose, selectedEars, selectedMouth, selectedSunglasses, selectedBand, selectedRibbon, selectedTie, selectedBow, selectedGlasses, selectedPiercing, selectedBarette};
		Prefs.PlayerPrefsX.SetIntArray(characterAssignment + "_avatar", prefs);
		bool[] options = new bool[]{ hasLongHair, hasShortHair,hasGlasses, hasBand, hasRibbon, hasTie, hasBow, hasPiercing, hasSunglasses, hasBarette };
		Prefs.PlayerPrefsX.SetBoolArray (characterAssignment + "_options", options);
		Debug.Log("Saving Character " + name);
		PlayerPrefs.SetString (characterAssignment, name);
	}

	public void loadCharacter() {
		Debug.Log ("Loading " + characterAssignment);
		name = PlayerPrefs.GetString (characterAssignment);
		Debug.Log("Has name of " + name);
		int[] prefs = Prefs.PlayerPrefsX.GetIntArray (characterAssignment + "_avatar");
		bool[] options = Prefs.PlayerPrefsX.GetBoolArray (characterAssignment + "_options");
		hasLongHair = options [0];
		hasShortHair = options [1];
		hasBand = options [2];
		hasRibbon = options [3];
		hasTie = options [4];
		hasBow = options [5];
		hasPiercing = options [6];
		hasSunglasses = options [7];
		hasBarette = options [8];

		baseSkinTone = prefs [0];
		tanTone = prefs [1];
		selectedFace = prefs [2];
		selectedHairColor = prefs [3];
		selectedEyeBrow = prefs [4];
		selectedBackgroundHair = prefs [5];
		selectedForegroundHair = prefs [6];
		selectedHairLine = prefs [7];
		selectedEyes = prefs [8];
		selectedIris = prefs [9];
		selectedNose = prefs [10];
		selectedEars = prefs [11];
		selectedMouth = prefs [12];
		selectedSunglasses = prefs [13];
		selectedBand = prefs [14];
		selectedRibbon = prefs [15];
		selectedTie = prefs [16];
		selectedBow = prefs [17];
		selectedGlasses = prefs[18];
		selectedPiercing = prefs[19];
		selectedBarette = prefs [20];
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
			PlayerPrefs.SetString (characterAssignment, name);
			Debug.Log ("NAME IS " + name);
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
			hasBand = true;
		} else {
			hasBand = false;
		}

		if (Random.Range (0, 2) == 1) {
			hasRibbon = true;
		} else {
			hasRibbon = false;
		}

		if (Random.Range (0, 2) == 1) {
			hasTie = true;
		} else {
			hasTie = false;
		}

		int typeOfGlasses = Random.Range (0, 3);
		switch (typeOfGlasses) {
		case 0:
			hasGlasses = false;
			hasSunglasses = false;
			break;
		case 1:
			hasGlasses = true;
			hasSunglasses = false;
			break;
		case 2:
			hasGlasses = false;
			hasSunglasses = true;
			break;
		}


		if (Random.Range (0, 2) == 1) {
			hasBow = true;
		} else {
			hasBow = false;
		}

		if (Random.Range (0, 2) == 1) {
			hasPiercing = true;
		} else {
			hasPiercing = false;
		}

		if (Random.Range (0, 2) == 1) {
			hasBarette = true;
		} else {
			hasBarette = false;
		}

		selectedHairColor = Random.Range (0, 8);
		baseSkinTone = Random.Range (0, 4);
		tanTone = 0;
//		PlayerPrefs.SetInt("selected hair color", selectedHairColor);
//		PlayerPrefs.SetInt ("selected skin tone", baseSkinTone);
	}


	public void loadStyles() {
			//TODO: When skin tone and color exports arrive, place each in a subfolder, store skin tone in playerprefs
			//face and skintone affected assets will ultimately be: Avatar/Face/<basetone>/<shade>/
			//other assets will be: Avatar/iris/<color>
			faceStyles = Resources.LoadAll<Sprite> ("Avatar/Face/" + baseSkinTone + "/" + tanTone);
			earStyles = Resources.LoadAll<Sprite> ("Avatar/Ears/" + baseSkinTone + "/" + tanTone);
			hairlineStyles = Resources.LoadAll<Sprite> ("Avatar/Hair/" + selectedHairColor + "/Line");
			backgroundHairStyles = Resources.LoadAll<Sprite> ("Avatar/Hair/" + selectedHairColor + "/Long");
			hairStyles = Resources.LoadAll<Sprite> ("Avatar/Hair/" + selectedHairColor + "/Short");
			eyeBrowStyles = Resources.LoadAll<Sprite> ("Avatar/Hair/" + selectedHairColor + "/Brows");
			eyeStyles = Resources.LoadAll<Sprite> ("Avatar/Eyes");
			irisStyles = Resources.LoadAll<Sprite> ("Avatar/Iris");
			noseStyles = Resources.LoadAll<Sprite> ("Avatar/Nose");
			mouthStyles = Resources.LoadAll<Sprite> ("Avatar/Mouth");
			sunglassStyles = Resources.LoadAll<Sprite> ("Avatar/Sunglasses");
			tieStyles = Resources.LoadAll<Sprite> ("Avatar/Ties");
			bandStyles = Resources.LoadAll<Sprite> ("Avatar/Bands");
			ribbonStyles = Resources.LoadAll<Sprite> ("Avatar/Ribbons");
			glassStyles = Resources.LoadAll<Sprite> ("Avatar/Glasses");
			baretteStyles = Resources.LoadAll<Sprite> ("Avatar/Barrette");
			bowStyles = Resources.LoadAll<Sprite> ("Avatar/Bow");
			piercingStyles = Resources.LoadAll<Sprite> ("Avatar/Piercings");
			
	}

	public void assignStyles() {
		if (hasShortHair) {
			foregroundHair.sprite = hairStyles [selectedForegroundHair];
		} 


		if (hasLongHair) {
			backgroundHair.sprite = backgroundHairStyles[selectedBackgroundHair];
		} 

		if (hasGlasses) {
			glasses.sprite = glassStyles[selectedSunglasses];
		} 
		if (hasBand) {
			band.sprite = bandStyles[selectedBand];
		} 
		if (hasRibbon) {
			ribbon.sprite = ribbonStyles[selectedRibbon];
		} 

		if (hasTie) {
			tie.sprite = tieStyles[selectedTie];
		} 

		if (hasSunglasses) {
			sunglasses.sprite = sunglassStyles[selectedSunglasses];
		}

		if (hasBarette) {
			barette.sprite = baretteStyles[selectedBarette];
		}

		if (hasPiercing) {
			piercing.sprite = piercingStyles[selectedPiercing];
		}

		if (hasBow) {
			bow.sprite = bowStyles[selectedBow];
		}

		foregroundHair.enabled = hasShortHair;
		backgroundHair.enabled = hasLongHair;

		glasses.enabled = hasGlasses;
		band.enabled = hasBand;
		ribbon.enabled = hasRibbon;
		tie.enabled = hasTie;
		bow.enabled = hasBow;
		barette.enabled = hasBarette;
		sunglasses.enabled = hasSunglasses;
		piercing.enabled = hasPiercing;


		face.sprite = faceStyles[selectedFace];
		ears.sprite = earStyles [selectedEars];
		hairLine.sprite = hairlineStyles [selectedHairLine];
		brows.sprite = eyeBrowStyles [selectedEyeBrow];
		eyes.sprite = eyeStyles [selectedEyes];
		iris.sprite = irisStyles [selectedIris];
		nose.sprite = noseStyles [selectedNose];
		mouth.sprite = mouthStyles [selectedMouth];
		                         
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
	}
	// Update is called once per frame
	void Update () {
	
	}
}
