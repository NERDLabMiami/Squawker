using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour
{
    public GameObject conversationContainer;
    public GameObject conversationTemplate;
    public string[] interventionNPC;
    public Sprite[] interventionNPCImages;
    private int interventionIndex = 1;
    public Player player;
    public bool startNewIntervention;
    private bool hasStartedGame = false;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!PlayerPrefs.HasKey("game started"))
        {
            PlayerPrefs.SetInt("game started", 1);
            AddTutorial(true);
        }
        else
        {
            AddTutorial(false);
            if (PlayerPrefs.GetInt("intervention index", 0) == 1)
            {
                startNewIntervention = true;
            }

        }

    }

    void AddTutorial(bool newMessage)
    {
        GameObject conversation = Instantiate(conversationTemplate, conversationContainer.gameObject.transform);
        conversation.GetComponent<Conversation>().characterName.text = interventionNPC[0];
        conversation.GetComponent<Conversation>().image.sprite = interventionNPCImages[0];


        if (newMessage)
        {
            conversation.GetComponent<Conversation>().messageCount.text = 1.ToString();
        }
        else
        {
            conversation.GetComponent<Conversation>().messageCount.transform.parent.gameObject.SetActive(false);

        }

        conversation.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            if (player)
            {
                player.chatWithCharacter(interventionNPC[0], 0);
            }
            else
            {
                Debug.LogError("Can't find player...trying again");
            }
        });
    }


    // Update is called once per frame
    void Update()
    {
        if (startNewIntervention)
        {
            GameObject conversation = Instantiate(conversationTemplate, conversationContainer.gameObject.transform);
            conversation.GetComponent<Conversation>().characterName.text = interventionNPC[interventionIndex];
            conversation.GetComponent<Conversation>().image.sprite = interventionNPCImages[interventionIndex];

            conversation.GetComponent<Conversation>().messageCount.text = 1.ToString();
            conversation.GetComponentInChildren<Button>().onClick.AddListener(() =>

            {
                player.chatWithCharacter(interventionNPC[interventionIndex], interventionIndex);
            });

            startNewIntervention = false;

        }
    }
}
