using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotNavigation : MonoBehaviour
{
    public GameObject[] dots;
    public Sprite selectedDot;
    public Sprite unselectedDot;
    private bool canChangeDots;
    private int activeDot;
    private int numberOfDots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextDot()
    {
        if(canChangeDots)
        {
            dots[activeDot].GetComponent<Image>().sprite = unselectedDot;
            activeDot++;
            if(activeDot >= numberOfDots)
            {
                activeDot = 0;
            }
            dots[activeDot].GetComponent<Image>().sprite = selectedDot;

        }
        else
        {
            Debug.Log("Not enough dots...");
        }
    }

    public void PreviousDot()
    {
        if (canChangeDots)
        {
            dots[activeDot].GetComponent<Image>().sprite = unselectedDot;
            if (activeDot > 0)
            {
                activeDot--;
            }
            else
            {
                activeDot = dots.Length - 1;
            }

            dots[activeDot].GetComponent<Image>().sprite = selectedDot;

        }
        else
        {
            Debug.Log("Not enough dots...");
        }
    }


    public void SetNumberOfDots(int n)
    {
        numberOfDots = n;
        switch(n)
        {
            case 1:
                //choose the middle dot
                activeDot = 1;
                canChangeDots = false;
                dots[1].GetComponent<Image>().sprite = selectedDot;
                dots[0].GetComponent<Image>().sprite = unselectedDot;
                dots[2].GetComponent<Image>().sprite = unselectedDot;
                dots[0].SetActive(false);
                dots[1].SetActive(true);
                dots[2].SetActive(false);

                break;
            case 2:
                //left to right, first one
                activeDot = 0;
                canChangeDots = true;
                dots[0].SetActive(true);
                dots[1].SetActive(true);
                dots[2].SetActive(false);
                dots[0].GetComponent<Image>().sprite = selectedDot;
                dots[1].GetComponent<Image>().sprite = unselectedDot;
                dots[2].GetComponent<Image>().sprite = unselectedDot;

                break;
            case 3:
                //left to right, first one
                activeDot = 0;
                canChangeDots = true;
                dots[1].SetActive(true);
                dots[2].SetActive(true);
                dots[0].SetActive(true);
                dots[0].GetComponent<Image>().sprite = selectedDot;
                dots[1].GetComponent<Image>().sprite = unselectedDot;
                dots[2].GetComponent<Image>().sprite = unselectedDot;

                break;
            default:
                Debug.Log("Only 1 to 3 dots are allowed...");
                break;
        }
    }

}
