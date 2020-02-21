using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Inbox : MonoBehaviour {
	public IncomingMessage messageTemplate;
    public GameObject responseTemplate;
	public GameObject messageContainer;
    public GameObject responseContainer;
    public GameObject responseOptions;
	public GameObject emptyMailboxMessage;
	public AudioClip notification;
	public AudioSource source;
    public string characterPath;
	private Player player;


	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    public void notify() {
		source.PlayOneShot(notification);
	}

	public void checkIfEmpty() {
		//TODO: Look at player
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	
			if (player.inbox.Count < 1) {
				emptyMailboxMessage.SetActive (true);
			}
			else {
				emptyMailboxMessage.SetActive (false);

			}

	}

	public void clear() {
		foreach (Transform child in messageContainer.transform)
		{
			Destroy(child.gameObject);
		}		
	}

	public void addMessage(Message msg)  {
		GameObject message = Instantiate(messageTemplate.gameObject);
        message.GetComponent<IncomingMessage>().setMessage(msg);
		message.transform.SetParent(messageContainer.transform, false);
	}

    public void addResponse(Response r)
    {
        GameObject response = Instantiate(responseTemplate, responseOptions.transform);
        response.GetComponent<ResponseOption>().response.text = r.text;
            response.GetComponentInChildren<Button>().onClick.AddListener(() => {
                Debug.Log("PATH: " + r.path + " MESSAGE: " + r.messageIndex + " BELIEF: " + r.belief);
                respond(r.path, r.messageIndex, r.belief);
            });


    }

    void respond(string path, int index, string belief)
    {
        responseContainer.SetActive(false);

        //TODO: Clear Options Children

        string[] res = StringArrayFunctions.getMessage(path);
        player.Chat("anxiety", res[1]);

        Debug.Log(path);

        
        
        //        GetComponent<PlayerBehavior>().trackEvent(2, "DLG", belief, StringArrayFunctions.getMessage(path)[0]);
        if (StringArrayFunctions.getMessage(path)[1].Contains("deadend"))
        {
            //Player chose a dead end, unhook to allow for a new conversation
            player.unhook();
        }
        else
        {
            Debug.Log("NOT A DEADEND: " + path);
        }
//        addMessage(path);
        //player.refreshInbox();
        //player.previewInbox.messageContainer.SetActive(true);
  //      Destroy(gameObject);

    }

}
