using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedBuilding : MonoBehaviour {

    public GameObject info;
    public Text infobar;
    public string message;

    // Use this for initialization
    void Start()
    {
        message += "\nLocked";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HoverEnter()
    {
        info.transform.localScale = new Vector3(3, 1, 1);
        infobar.text = message;
    }

    public void HoverExit()
    {
        info.transform.localScale = new Vector3(0, 0, 0);
        infobar.text = "";
    }
}
