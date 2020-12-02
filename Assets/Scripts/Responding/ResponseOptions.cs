using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseOptions : MonoBehaviour
{

    public GameObject nextButton;
    public GameObject previousButton;
    private int optionIndex;
    public DotNavigation dots;
    public List<ResponseOption> options;
    public bool TypeOutResponse;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void togglePagination(bool active)
    {
        nextButton.SetActive(active);
        previousButton.SetActive(active);

    }

    public void CheckForResponseOptions()
    {
        Debug.Log("RESPONSE OPTIONS: " + transform.childCount);
        if (transform.childCount > 1)
        {
            options.Clear();
            options.AddRange(GetComponentsInChildren<ResponseOption>());
            optionIndex = 0;
            togglePagination(true);
            SetAllResponseOptionsInactive();
            dots.SetNumberOfDots(transform.childCount);
            Debug.Log("Setting Option " + optionIndex + " Active");
            options[optionIndex].gameObject.SetActive(true);
            TypeOut();
        }
        else
        {
            togglePagination(false);
            dots.SetNumberOfDots(1);
            GetComponentInChildren<ResponseOption>().response.GetComponent<UITextTypeWriter>().TypeText();
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
        TypeOut();
    }

    private void TypeOut()
    {
            options[optionIndex].GetComponent<ResponseOption>().response.GetComponent<UITextTypeWriter>().TypeText();
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
        TypeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
