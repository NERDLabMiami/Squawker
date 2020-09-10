using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour
{
    public GameObject conversationContainer;
    public GameObject conversationTemplate;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        PlayerPrefs.GetInt("direct messages", 0);
        //loop through JSON intervention characters
        string[] chats = PlayerPrefsX.GetStringArray("chats");
        if (chats.Length > 0)
        {
            for (int i = 0; i < chats.Length; i++)
            {
                GameObject conversation = Instantiate(conversationTemplate, conversationContainer.gameObject.transform);
                conversation.GetComponent<Conversation>().characterName.text = chats[i];
                conversation.GetComponent<Conversation>().messageCount.text = 1.ToString(); 
                conversation.GetComponentInChildren<Button>().onClick.AddListener(() =>

               {
                   player.chatWithCharacter(conversation.GetComponent<Conversation>().characterName.text);
               });
            }
        }
        else
        {
            Debug.Log("No Direct Messages to Player Created Yet");
        }


        //check against activated conversations

        //populate number of messages received/not read count 


        //create click behavior to load proper chat scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
