using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    public Text body;
    private Message message;
    public Inbox inbox;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void scrollToBottom()
    {
        inbox.m_ScrollRect.normalizedPosition = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText(string msg)
    {
        body.text = msg;
    }


}
