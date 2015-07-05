using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewMessage : MonoBehaviour {
	public Image profilePic;
	public Text sender;
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
		response.GetComponent<Button>().onClick.AddListener(() => {respond(r.path, r.messageIndex);});	
		response.transform.SetParent(responseContainer, false);

	}

	void respond(string path, int index) {
		Debug.Log ("Called from full message with path " + path);
		player.removeMessage(index);
		player.addMessage(path);
		player.refreshInbox();
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
