using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicToggle : MonoBehaviour {

    public GameObject UserInterface;
    bool isUIon;
    public GameObject CinematicTextOn;
    public GameObject CinematicTextOff;
    public GameObject warning;


    // Use this for initialization
    void Start () {
        UserInterface.SetActive(true);
        isUIon = true;
        Animation On = CinematicTextOn.GetComponent<Animation>();
        Animation Off = CinematicTextOff.GetComponent<Animation>();
    }

    void Update()
    {
        UItoggle();
    }

    void UItoggle()
    {
        if (Input.GetKeyDown("c"))
        {

            if (isUIon == true)
            {
                UserInterface.SetActive(false);
                isUIon = false;
                CinematicTextOn.SetActive(true);
                CinematicTextOff.SetActive(false);
            }

            else
            {
                UserInterface.SetActive(true);
                isUIon = true;
                CinematicTextOff.SetActive(true);
                CinematicTextOn.SetActive(false);
                warning.SetActive(false);
            }
        }
    }
}
