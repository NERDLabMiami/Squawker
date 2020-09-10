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
    
 

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    public void notify() {
		source.PlayOneShot(notification);
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
		message.transform.SetParent(messageContainer.transform, false);
        m_ScrollRect.normalizedPosition = new Vector2(0, 0);
        //   float backup = m_ScrollRect.verticalNormalizedPosition;
     //   StartCoroutine(ApplyScrollPosition(m_ScrollRect, backup));
	}

    public void addResponseToChatLog(string response)
    {
        GameObject r = Instantiate(playerMessageTemplate);
        PlayerMessage message = r.GetComponent<PlayerMessage>();
        message.setText(response);
        r.transform.SetParent(messageContainer.transform, false);
        //        float backup = m_ScrollRect.verticalNormalizedPosition;
        //        StartCoroutine(ApplyScrollPosition(m_ScrollRect, backup));
        m_ScrollRect.normalizedPosition = new Vector2(0, 0);

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
            GameObject response = Instantiate(responseTemplate, responseOptions.transform);
            response.GetComponent<ResponseOption>().response.text = r.text;
            response.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                Debug.Log("PATH: " + r.path + " MESSAGE: " + r.messageIndex + " BELIEF: " + r.belief);
               
                addResponseToChatLog(r.text);
                //store conversation in player prefs string array. Example anxiety = "Hey, you've been missing class a lot. You alright? - Are you really sure about this?"
                string[] previous_responses = PlayerPrefsX.GetStringArray("responses_" + player.getCharacter());
                List<string> previous_responses_list = previous_responses.ToList();
                previous_responses_list.Add(r.text);
                PlayerPrefsX.SetStringArray("responses_" + player.getCharacter(), previous_responses_list.ToArray());
//                PlayerPrefs.SetString(r.to + "_" + StringArrayFunctions.getMessage(r.path)[0] + "_response", r.path);
                respond(r.path, r.messageIndex, r.belief);
            });
        }

    }

    void respond(string path, int index, string belief)
    {
        //        responseContainer.SetActive(false);
        responseOptions.GetComponent<ResponseOptions>().options.Clear();
        responseOptions.GetComponent<ResponseOptions>().togglePagination(false);
        string[] res = StringArrayFunctions.getMessage(path);
        player.Chat(player.getCharacter(), res[1]);

        Debug.Log(path);

        
        //        GetComponent<PlayerBehavior>().trackEvent(2, "DLG", belief, StringArrayFunctions.getMessage(path)[0]);
        if (StringArrayFunctions.getMessage(path)[1].Contains("deadend"))
        {
            //Player chose a dead end, unhook to allow for a new conversation
//            player.unhook();
        }
        else
        {
  //          Debug.Log("NOT A DEADEND: " + path);
        }
        //        addMessage(path);
        //player.refreshInbox();
        //player.previewInbox.messageContainer.SetActive(true);
        //      Destroy(gameObject);




        respondButton.SetActive(true);
    }

}
