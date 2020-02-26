using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    public Image profilePhoto;
    public Text profileName;
    public Text timePosted;
    public Text status;
    public Text comments;
    private int commentCount;
    public float age;

    
    // Start is called before the first frame update
    void Start()
    {
        age = 0;
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        if((int)age%60 == 0)
        {
            //a minute has passed
            UpdatePostTime();
        }
    }

    private void UpdatePostTime()
    {
        int minutesPassed = (int)age / 60;
        if (minutesPassed > 0)
        {
            if (minutesPassed == 1)
            {
                timePosted.text = minutesPassed.ToString("0 minute ago");
            }
            else
            {
                timePosted.text = minutesPassed.ToString("0 minutes ago");

            }
        }
    }

    public void SetCommentCount(int amount)
    {
        commentCount += amount;
        comments.text = commentCount.ToString("0");
    }
}
