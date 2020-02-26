using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class Feed : MonoBehaviour
{
    public TextAsset feed;
    public JSONNode json;
    public GameObject postTemplate;
    public GameObject commentTemplate;
    public GameObject postContainer;
    public ResponseOptions comments;
    public Text notifications;
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
            //

            if(message.Length == 1)
            {
                responses = json[message[0]]["posts"][randomCharacterPostIndex]["responses"];
            }
            else
            {
                responses = json[message[0]][message[1]]["responses"];

            }

            for (int i = 0; i < responses.Count; i++)
            {
                string[] param = StringArrayFunctions.getMessage(responses[i]["path"]);

                GameObject comment = Instantiate(commentTemplate, comments.gameObject.transform);
                comment.GetComponent<ResponseOption>().response.text = responses[i]["response"];
                comment.GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    //POST COMMENT CLICK
                    post.GetComponent<Post>().SetCommentCount(1);
                    comments.togglePagination(false);
                    comments.gameObject.SetActive(false);
                    //check if there's a DM
                    if (param[0].Contains("chat")) { 
                        notifications.text = "1";
                    }
                    //REMOVE PREVIOUS RESPONSE OPTIONS
                    foreach (Transform child in comments.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                });

            }

            comments.gameObject.SetActive(true);
            comments.CheckForResponseOptions();
        });

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
