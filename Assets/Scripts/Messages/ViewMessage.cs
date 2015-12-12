using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewMessage : MonoBehaviour {
	public Image profilePic;
	public Character character;
	public Text alias;
	public Text body;
	public Transform responseContainer;
	public GameObject responseTemplate;

	private Player player;
	private string characterEpiloguePath;
	private string epilogueStoryPath;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	private void startEpilogue() {

	}

	public void addResponse(Response r) {
		GameObject response = Instantiate(responseTemplate);
		response.GetComponent<ResponseOption>().response.text = r.text;
		int type = offerType (response, r);
		if (type == 5) {
			//TODO: parse path sender and cue epilogue
			response.GetComponent<Button> ().onClick.AddListener (() => {
				player.removeMessage (r.messageIndex);
				Inbox inbox = GameObject.FindGameObjectWithTag("Inbox").GetComponent<Inbox>();
				string characterPath = getStringFromResponse(r.path,0);
				string storyPath = getStringFromResponse(r.path, 2);
				inbox.populateEpilogue(characterPath, storyPath, character.name);
				inbox.epilogue.npc.characterAssignment = character.characterAssignment;
				inbox.epilogue.cue ();

//				Epilogue e = GameObject.Find("/Canvas/Epilogue").GetComponent<Epilogue>();
//				e.npc = character;
//				e.cue();

			});

		}
		else if (type != -1) {
			response.GetComponent<Button> ().onClick.AddListener (() => {
				player.removeMessage (r.messageIndex);
				Application.LoadLevel(type);
			});
		} else {
			response.GetComponent<Button> ().onClick.AddListener (() => {
				respond (r.path, r.messageIndex);
				});	

			}
		response.transform.SetParent(responseContainer, false);

	}

	int offerType(GameObject obj, Response r) {
		string[] pathArray = StringArrayFunctions.getMessage(r.path);
		if (pathArray [0] == "tanning") {
			return 2;
		}
		if (pathArray [0] == "love") {
			return 3;
		}
		
		if (pathArray [0] == "dermatologist") {
			return 4;
		}
		if (pathArray.Length > 1) {
			//has a character attached to it
			if (pathArray [1] == "epilogue") {

				Debug.Log("Epilogue: " + r.path);

				return 5;
			}
		}
	return -1;

	}

	string getStringFromResponse(string path, int index) {
		string[] pathArray = StringArrayFunctions.getMessage(path);
		Debug.Log("PATH IS " + pathArray.Length + " LONG");
		Debug.Log("PATH IS " + path);

		return pathArray[index];
	}
	bool isOffer(GameObject obj, Response r) {
		string[] pathArray = StringArrayFunctions.getMessage(r.path);
		if (pathArray [0] == "tanning") {
			obj.GetComponent<Button> ().onClick.AddListener (() => {
				player.removeMessage (r.messageIndex);
				Application.LoadLevel (1);
			});
			return true;
		}
		if (pathArray [0] == "love") {
			obj.GetComponent<Button> ().onClick.AddListener (() => {
				player.removeMessage (r.messageIndex);
				Application.LoadLevel (2);
			});
			return true;
		}

		if (pathArray [0] == "dermatologist") {
			Debug.Log("Skin Exam");
			obj.GetComponent<Button> ().onClick.AddListener (() => {
				player.removeMessage (r.messageIndex);
				Application.LoadLevel (3);
			});
			return true;
		}


		return false;
	}

	void respond(string path, int index) {
		player.takeAction (true);
		player.removeMessage(index);
		//TODO: Check threshold requirements if needed in mid conversation to add/remove response time
		player.addMessage(path);
		player.refreshInbox();
		player.previewInbox.messageContainer.SetActive(true);
		Destroy (gameObject);

	}

	public void ignore() {
		player.previewInbox.messageContainer.SetActive(true);
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDisable() {
//		GameObject.FindGameObjectWithTag("Inbox").GetComponent<Inbox>().messageContainer.SetActive(true);
		Destroy (this.gameObject);
	}
}
