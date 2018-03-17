using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHoverDisplayInfo : MonoBehaviour {

    public Text infobar;
    public string message;

    void OnMouseOver()
    {
        infobar.text = message;
    }

    void OnMouseExit()
    {
        infobar.text = "";
    }
}
