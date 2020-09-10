using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Linq;

public class Feed : MonoBehaviour
{
    public TextAsset feed;
    public JSONNode json;
    public GameObject postTemplate;
    public GameObject commentTemplate;
    public GameObject postContainer;
    public ResponseOptions comments;
    public GameObject footer;
    public GameObject notifications;
    public float timeBetweenPosts;
    private float timeUntilNextPost;
    private int randomCharacterPostIndex;
    private JSONNode responses;

    // Start is called before the first frame update

    void Start()
    {
        json = JSON.Parse(feed.ToString());
        NewStatusUpdate("interventions/anxiety");
        timeUntilNextPost = Time.time + timeBetweenPosts;        
    }


    public void NewStatusUpdate(string path)
    {
        GameObject post = Instantiate(postTemplate, postContainer.transform);
        post.GetComponent<Post>().timePosted.text = "Now";
        string[] message = StringArrayFunctions.getMessage(path);
        if (message.Length == 2)
        {
            //INTERVENTION
            post.GetComponent<Post>().status.text = json[message[0]][message[1]]["post"];
            post.GetComponent<Post>().profileName.text = json[message[0]][message[1]]["name"];
            //            post.GetComponent<Post>().profilePhoto = IMAGE;

        }
        else if(message.Length == 1)
        {
            //RANDOM
            int randomCharacterNameIndex = Random.Range(0, json[message[0]]["names"].Count);
            randomCharacterPostIndex = Random.Range(0, json[message[0]]["posts"].Count);
            post.GetComponent<Post>().status.text = json[message[0]]["posts"][randomCharacterPostIndex]["post"];
            post.GetComponent<Post>().profileName.text = json[message[0]]["names"][randomCharacterNameIndex];

        }
        post.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            //Pop Up Footer

            footer.SetActive(true);




            //Populate Responses
            if (message.Length == 1)
            {
                responses = json[message[0]]["posts"][randomCharacterPostIndex]["responses"];
            }
            else
            {
                responses = json[message[0]][message[1]]["responses"];

            }
            if (!post.GetComponent<Post>().responsesPopulated)
            {
                for (int i = 0; i < responses.Count; i++)
                {
                    string[] param = StringArrayFunctions.getMessage(responses[i]["path"]);

                    GameObject comment = Instantiate(commentTemplate, comments.gameObject.transform);
                    comment.GetComponent<ResponseOption>().response.text = responses[i]["response"];
                    comment.GetComponent<ResponseOption>().postCommentButton = post.GetComponent<Post>().commentButton;
                    comment.GetComponentInChildren<Button>().onClick.AddListener(() =>

                    {
                        comment.GetComponent<ResponseOption>().clicked();

                        //POST COMMENT CLICK
                        post.GetComponent<Post>().SetCommentCount(1);
                        //                        comments.togglePagination(false);
                        //                        comments.gameObject.SetActive(false);
                        //check if there's a DM
                        if (param[0].Contains("chat"))
                        {
                            string[] chats = PlayerPrefsX.GetStringArray("chats");
                            List<string> chats_list = chats.ToList();
                            if (!chats_list.Contains(param[2]))
                            {
                                Debug.Log("This chat doesn't exist yet, adding it to chat list");
                                chats_list.Add(param[2]);
                                PlayerPrefsX.SetStringArray("chats", chats_list.ToArray());
                            }
                            else
                            {
                                Debug.Log("This chat already exists, not adding it to the list");
                            }
                        notifications.SetActive(true);
                    }
                    //REMOVE PREVIOUS RESPONSE OPTIONS
                    foreach (Transform child in comments.transform)
                        {
                            GameObject.Destroy(child.gameObject);
                        }

                    footer.SetActive(false);                    
                    });
                }
                post.GetComponent<Post>().responsesPopulated = true;
                comments.CheckForResponseOptions();
                comments.gameObject.SetActive(true);
            }









        });

    }


    public void PopulateResponses()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timeUntilNextPost <= Time.time)
        {

            NewStatusUpdate("random");



            timeUntilNextPost = Time.time + timeBetweenPosts;
        }
    }
}
