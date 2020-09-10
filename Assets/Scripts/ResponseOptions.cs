using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseOptions : MonoBehaviour
{

    public GameObject nextButton;
    public GameObject previousButton;
    private int optionIndex;
//    public ResponseOptions[] options;
    public List<ResponseOption> options;


    // Start is called before the first frame update
    void Start()
    {
//        nextButton.SetActive(false);
//        previousButton.SetActive(false);
//        gameObject.SetActive(false);
    }

    public void togglePagination(bool active)
    {
        nextButton.SetActive(active);
        previousButton.SetActive(active);

    }

    public void CheckForResponseOptions()
    {
        Debug.Log("RESPONSE OPTIONS: " + transform.childCount);
        if(transform.childCount > 1)
        {
            options.Clear();
            options.AddRange(GetComponentsInChildren<ResponseOption>());
            optionIndex = 0;
            togglePagination(true);
            SetAllResponseOptionsInactive();
            Debug.Log("Setting Option " + optionIndex + " Active");
            options[optionIndex].gameObject.SetActive(true);
        }
        else
        {
            togglePagination(false);
        }
    }

    public void SetAllResponseOptionsInactive()
    {
        for (int i = 0; i < options.Count; i++)
        {
            options[i].gameObject.SetActive(false);
        }

    }

    public void Next()
    {
        SetAllResponseOptionsInactive();
        optionIndex++;

        if (optionIndex >= transform.childCount)
        {
            optionIndex = 0;
        }

        options[optionIndex].gameObject.SetActive(true);

    }

    public void Previous()
    {
        for (int i = 0; i < options.Count; i++)
        {
            options[i].gameObject.SetActive(false);
        }

        optionIndex--;

        if (optionIndex < 0)
        {
            optionIndex = options.Count - 1;
        }

        options[optionIndex].gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
