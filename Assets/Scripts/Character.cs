using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	public Image backgroundHair;
	public Image face;
	public Image hairLine;
	public Image eyes;
	public Image foregroundHair;
	public Image iris;
	public Image nose;
	public Image mouth;
	private Sprite[] backgroundHairStyles;
	private Sprite[] faceStyles;
	private Sprite[] hairlineStyles;
	private Sprite[] eyeStyles;
	private Sprite[] hairStyles;
	private Sprite[] irisStyles;
	private Sprite[] noseStyles;
	private Sprite[] mouthStyles;
	private int selectedBackgroundHair;
	private int selectedFace;
	private int selectedHairLine;
	private int selectedEyes;
	private int selectedForegroundHair;
	private int selectedIris;
	private int selectedNose;
	private int selectedMouth;

//	private Image 
	// Use this for initialization
	void Start () {
		loadStyles ();
		generateAvatar ();
		assignStyles ();

	}

	public Sprite getHairStyle() {
		return backgroundHairStyles [Random.Range (0, backgroundHairStyles.Length)];
	}

	private void loadStyles() {
			//TODO: When skin tone and color exports arrive, place each in a subfolder, store skin tone in playerprefs
			//face and skintone affected assets will ultimately be: Avatar/Face/<basetone>/<shade>/
			//other assets will be: Avatar/iris/<color>
			backgroundHairStyles = Resources.LoadAll<Sprite> ("Avatar/Long Hair");
			faceStyles = Resources.LoadAll<Sprite> ("Avatar/Face");
			hairlineStyles = Resources.LoadAll<Sprite> ("Avatar/Hair Line");
			eyeStyles = Resources.LoadAll<Sprite> ("Avatar/Eyes");
			hairStyles = Resources.LoadAll<Sprite> ("Avatar/Short Hair");
			irisStyles = Resources.LoadAll<Sprite> ("Avatar/Iris");
			noseStyles = Resources.LoadAll<Sprite> ("Avatar/Nose");
			mouthStyles = Resources.LoadAll<Sprite> ("Avatar/Mouth");

	}

	public void assignStyles() {
		backgroundHair.sprite = backgroundHairStyles[selectedBackgroundHair];
		face.sprite = faceStyles[selectedFace];
		hairLine.sprite = hairlineStyles [selectedHairLine];
		eyes.sprite = eyeStyles [selectedEyes];
		foregroundHair.sprite = hairStyles [selectedForegroundHair];
		iris.sprite = irisStyles [selectedIris];
		nose.sprite = noseStyles [selectedNose];
		mouth.sprite = mouthStyles [selectedMouth];
		                         
	}

	public void generateAvatar() {
			selectedBackgroundHair = Random.Range (0, backgroundHairStyles.Length);
			selectedFace = Random.Range (0, faceStyles.Length);
			selectedHairLine = Random.Range (0, hairlineStyles.Length);
			selectedEyes = Random.Range (0, eyeStyles.Length);
			selectedForegroundHair = Random.Range (0, hairStyles.Length);
			selectedIris = Random.Range (0, irisStyles.Length);
			selectedNose = Random.Range (0, noseStyles.Length);
			selectedMouth = Random.Range (0, mouthStyles.Length);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
