using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Inbox : MonoBehaviour {
	public IncomingMessage messageTemplate;
    public GameObject responseTemplate;
    public GameObject playerMessageTemplate;
	public GameObject messageContainer;
    public GameObject responseContainer;
    public GameObject responseOptions;
	public GameObject emptyMailboxMessage;
    public GameObject respondButton;
    public ScrollRect m_ScrollRect;
	public AudioClip notification;
	public AudioSource source;
    public string characterPath;
	private Player player;
    public bool waitingForResponse;
    public bool waitingForNPCMessage;
    public bool responsesPopulated;

    private string responseText;
    private string responsePath;
    private int    responseIndex;
    private string responseBelief;
    private float responseTimer;


	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void Update()
    {

        if (waitingForNPCMessage)
        {
            if (responseTimer <= Time.time)
            {
                Debug.Log("responding now");
                respond(responsePath, responseIndex, responseBelief);
                waitingForNPCMessage = false;
            }
            else
            {
                Debug.Log("Counting down...");
            }
        }

        if (waitingForResponse)
        {
            if (!responsesPopulated)
            {
                responseOptions.transform.parent.gameObject.SetActive(true);

                player.PopulateResponses();

                responsesPopulated = true;
            }


        }

    }

    public void notify() {
		source.PlayOneShot(notification);
	}

    public void scrollToBottom()
    {
        m_ScrollRect.normalizedPosition = new Vector2(0, 0);

    }

    public void checkIfEmpty() {
		//TODO: Look at player
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	/*
			if (player.inbox.Count < 1) {
				emptyMailboxMessage.SetActive (true);
			}
			else {
				emptyMailboxMessage.SetActive (false);

			}
            */
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
        message.GetComponent<IncomingMessage>().inbox = this;
		message.transform.SetParent(messageContainer.transform, false);
	}

    public void addResponseToChatLog(string response)
    {
        GameObject r = Instantiate(playerMessageTemplate);
        PlayerMessage message = r.GetComponent<PlayerMessage>();
        message.setText(response);
        message.inbox = this;
        r.transform.SetParent(messageContainer.transform, false);

    }

    IEnumerator ApplyScrollPosition(ScrollRect sr, float verticalPos)
    {
        yield return new WaitForEndOfFrame();
        sr.verticalNormalizedPosition = verticalPos;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sr.transform);
    }

    public void addResponse(Response r)
    {
       
        string[] param = StringArrayFunctions.getMessage(r.path);
        Debug.Log("param count: " + param.Length);
        if (param.Length > 2)
        {
            //double check param 2 for "resolved"
            Debug.Log("HOORAY! ALERT FEED UPDATE!");
        }
        else
        {
            Debug.Log("Adding Response");
            //show response in chat interface
            GameObject response = Instantiate(responseTemplate, responseOptions.transform);
            response.GetComponent<ResponseOption>().response.text = r.text;
            response.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                Debug.Log("PATH: " + r.path + " MESSAGE: " + r.messageIndex + " BELIEF: " + r.belief);

                responseText = r.text;
                responsePath = r.path;
                responseIndex = r.messageIndex;
                responseBelief = r.belief;
                
                addResponseToChatLog(r.text);

                //store conversation in player prefs string array. Example anxiety = "Hey, you've been missing class a lot. You alright? - Are you really sure about this?"
                string[] previous_responses = PlayerPrefsX.GetStringArray("responses_" + player.getCharacter());
                List<string> previous_responses_list = previous_responses.ToList();
                previous_responses_list.Add(r.text);
                PlayerPrefsX.SetStringArray("responses_" + player.getCharacter(), previous_responses_list.ToArray());
                //                PlayerPrefs.SetString(r.to + "_" + StringArrayFunctions.getMessage(r.path)[0] + "_response", r.path);
                //set timer for response
                responseTimer = Time.time + Random.Range(1.5f, 3f);
                //cleanup option display
                responseOptions.GetComponent<ResponseOptions>().options.Clear();
                responseOptions.GetComponent<ResponseOptions>().togglePagination(false);
                responseOptions.transform.parent.gameObject.SetActive(false);
                waitingForNPCMessage = true;
            });
        }

    }

    void respond(string path, int index, string belief)
    {
        //        responseContainer.SetActive(false);
        string[] res = StringArrayFunctions.getMessage(path);
        player.SendMessageToPlayer(player.getCharacter(), res[1]);
        player.ClearResponseOptions();

        Debug.Log(path);

        
        //        GetComponent<PlayerBehavior>().trackEvent(2, "DLG", belief, StringArrayFunctions.getMessage(path)[0]);
        if (StringArrayFunctions.getMessage(path)[1].Contains("deadend"))
        {
            //Player chose a dead end, unhook to allow for a new conversation
        }

        respondButton.SetActive(true);
    }

}
