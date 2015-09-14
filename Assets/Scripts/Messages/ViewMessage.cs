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

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	public void addResponse(Response r) {
		GameObject response = Instantiate(responseTemplate);
		response.GetComponent<ResponseOption>().response.text = r.text;
		if (!isOffer(response, r)) {
			response.GetComponent<Button> ().onClick.AddListener (() => {
				respond (r.path, r.messageIndex);});	
		}
		else {
			//LOADING HAPPENS IN isOffer, should be moved here.
		}
		response.transform.SetParent(responseContainer, false);

	}

	bool isOffer(GameObject obj, Response r) {
//		if (int.Parse(messageParts[2]) <= 0) {
		string[] pathArray = StringArrayFunctions.getMessage(r.path);
		if (pathArray [0] == "tanning") {
			Debug.Log("Tanning Offer");
			obj.GetComponent<Button> ().onClick.AddListener (() => {
				player.removeMessage (r.messageIndex);
				Debug.Log ("Go to TanLines Mini Game");
				Application.LoadLevel (1);
			});
			return true;
		}
		if (pathArray [0] == "love") {
			Debug.Log("Love Quotient Offer");
			obj.GetComponent<Button> ().onClick.AddListener (() => {
				player.removeMessage (r.messageIndex);
				Debug.Log ("Go to Love Q Mini Game");
				Application.LoadLevel (2);
			});
			return true;
		}

		if (pathArray [0] == "dermatologist") {
			Debug.Log("Skin Exam");
			obj.GetComponent<Button> ().onClick.AddListener (() => {
				player.removeMessage (r.messageIndex);
				Debug.Log ("Go to Skin Exam");
				Application.LoadLevel (3);
			});
			return true;
		}


		return false;
	}

	void respond(string path, int index) {
		Debug.Log ("Called from full message with path " + path);
		player.removeMessage(index);
		//TODO: Check threshold requirements if needed in mid conversation to add/remove response time
		player.addMessage(path);
		player.refreshInbox();
		Debug.Log("Should activate message container...");
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
		Debug.Log("Destroying Object");
//		GameObject.FindGameObjectWithTag("Inbox").GetComponent<Inbox>().messageContainer.SetActive(true);
		Destroy (this.gameObject);
	}
}
