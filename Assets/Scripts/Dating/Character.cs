using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class Character : MonoBehaviour {

    /*
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
	*/
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
        /*
        if (face.gameObject.activeSelf && ears.gameObject.activeSelf && nose.gameObject.activeSelf && mouth.gameObject.activeSelf && iris.gameObject.activeSelf) {
			return true;
		}
		else {
			return false;
		}
        */
        return true;
	}

    /*
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
    */
	public void assign(string character = null) {
		Debug.Log ("Character assignment: " + character);
		if (character != null) {
			characterAssignment = character;
		}
		if (PlayerPrefs.HasKey (characterAssignment)) {
			Debug.Log("Has Character Assignment for " + characterAssignment);
			loadCharacter();
		
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

		//assign sprites
		if (characterAssignment == "new" || characterAssignment == "me") {
			//get assignments
			generateAvatar(false);
		}
		else {
			//get assignments
			generateAvatar (true);
			//set sprites
		}

		//set paths

		//save
		if (save) {
			saveCharacter();
		}
	}
	
	public string getCharacterAssignment() {
		return characterAssignment;
	}


	public void updateCharacter() {
		saveCharacter ();
}

	public void reloadCharacter() {
		if (PlayerPrefs.HasKey (characterAssignment)) {
			loadCharacter();
		} else {
			Debug.Log("Character assignment missing");
		}
	}

	public void saveCharacter(bool disableAccessories = false) {
        json = JSON.Parse(characters.ToString());
		Debug.Log ("NUMBER IN CHARACTER " + characterAssignment + ":"+ json[characterAssignment]["character_type"].Count);
		characterType = json[characterAssignment]["character_type"];

		PlayerPrefs.SetString (characterAssignment + "_type", characterType);
		Debug.Log ("Character: " + characterAssignment + " Character Type: " + characterType);

        PlayerPrefs.SetString (characterAssignment + "_type", "C22");

		PlayerPrefs.SetString (characterAssignment, characterName);
	}

	public void loadCharacter() {
		name = PlayerPrefs.GetString (characterAssignment);
		bool[] options = Prefs.PlayerPrefsX.GetBoolArray (characterAssignment + "_options");
		characterType = PlayerPrefs.GetString (characterAssignment + "_type");
		getPaths();


	}

	public void getPaths() {
		string[] assignedSprites = Prefs.PlayerPrefsX.GetStringArray(characterAssignment + "_avatar_paths");
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

	public int assignFizzle() {
		string[] fizzles = Prefs.PlayerPrefsX.GetStringArray("fizzles");
		Debug.Log("Have " + fizzles.Length + " fizzles");
		if (fizzles.Length > 0) {
			List<string> list = new List<string> (fizzles);
			int selectedCharacter = Random.Range (0, list.Count);
			characterAssignment = list [selectedCharacter];
			list.RemoveAt (selectedCharacter);
			Prefs.PlayerPrefsX.SetStringArray ("fizzles", list.ToArray ());
			PlayerPrefs.SetString (characterAssignment, characterName);
			Debug.Log ("Setting Character Type " + characterAssignment + " for " + characterName);
		}
		return fizzles.Length;
	}

	public void assignName(string gender) {
		int numNames = json["names"][gender].Count;
		name = json["names"][gender][Random.Range(0,numNames)];
		characterName = name;
	}


	

	public void generateAvatar(bool accessories = true) {
	}
	// Update is called once per frame
}
