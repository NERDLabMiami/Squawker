using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{

    public string sender;
    public float timeUntilNotificationDisappears;
    public Text notice;
    private bool notificationSent = false;

    void Start()
    {
        notice = GetComponentInChildren<Text>();
    }
    // Start is called before the first frame update

    public void SetNotice(string _notice, float t)
    {
        notice.text = _notice;
        timeUntilNotificationDisappears = Time.time + t;
        notificationSent = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(notificationSent)
        {
            if (Time.time > timeUntilNotificationDisappears)
            {
                Destroy(this.gameObject);
            }

        }

    }
}
