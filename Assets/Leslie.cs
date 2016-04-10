using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Leslie : MonoBehaviour {
	public Text dialogBox; 
	public GameObject dialogue;
	public int timeBetweenDialogueChange;
	private float nextDialogueTime;
	private bool showingDialogueBox;
	private string[] makingMatchDialogue = {"I think you're going to like this one...", "Oh, I hope you like the match I found for you!", "I love finding matches for people like you!"};
	private string[] winkedDialogue = {"Quite dreamy!", "I'd wink too!", "Oh yes, that's a match made in heaven.", "Go get 'em tiger!", "You're perfect for each other!"};
	private string[] passedDialogue = {"Just kidding!", "I need to work on my algorithm!", "What!? They're perfect for you!", "Don't be so picky!", "Aren't you picky...", "What if you just passed up your soul mate!", "Give love a chance!"};
	private string[] styleChangeDialogue = {"Oh, I like your style!", "Looking good!", "Me likey!", "You're gonna turn some heads for sure.", "YAASSSS!", "Love the look!", "Yes! Yes! A thousand times yes!"};
	private string[] styleDidntChangeDialogue = {"I like you just the way you are.", "Sticking to that look, eh?","I don't care what the say, that look still works!"};
	private string[] nudgeMessages = {"I hear that people with glasses like the bare look...", "You want me to find a match for you or what?", "Are you ignoring me?", "I'm pretty good at LoveQ...", "If you need a refresher on things, just let me know.", "I'm here to help, if you need me.", "Don't mind me, I'm just chilling.", "I'll just be over here perfecting my algorithm...", "Do you ever feel, like a plastic bag, drifting through the wind...", "Maybe you should change things up and update your style...", "Let me know if you need anything.", "zzz..."};

	// Use this for initialization
	void Start () {
		nextDialogueTime = Time.time + timeBetweenDialogueChange;
	}
	
	// Update is called once per frame
	void Update () {
		if (nextDialogueTime < Time.time && !showingDialogueBox) {
			//TODO: Pick random dialogue
			int selectedDialogue = Random.Range(0, nudgeMessages.Length);
			showMessage(nudgeMessages[selectedDialogue]);
		}
	}

	private void showMessage(string msg) {
		showingDialogueBox = true;
		dialogBox.text = msg;
		GetComponent<Animator>().SetTrigger("popup");
		nextDialogueTime = nextDialogueTime + timeBetweenDialogueChange;
	}

	public void hide() {
		nextDialogueTime = nextDialogueTime + timeBetweenDialogueChange;
		showingDialogueBox = false;

	}

	public void makeMatch() {
		int selectedDialogue = Random.Range(0, makingMatchDialogue.Length);
		showMessage(makingMatchDialogue[selectedDialogue]);
	}

	public void updateStyle() {
		int selectedDialogue = Random.Range(0, styleChangeDialogue.Length);
		showMessage(styleChangeDialogue[selectedDialogue]);
	}

	public void noUpdateStyle() {
		int selectedDialogue = Random.Range(0, styleDidntChangeDialogue.Length);
		showMessage(styleDidntChangeDialogue[selectedDialogue]);

	}

	public void winked() {
		int selectedDialogue = Random.Range(0, winkedDialogue.Length);
		showMessage(winkedDialogue[selectedDialogue]);

	}

	public void passed() {
		int selectedDialogue = Random.Range(0, passedDialogue.Length);
		showMessage(passedDialogue[selectedDialogue]);

	}
		
}
