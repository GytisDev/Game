using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour {

    public GameObject UnlockText;

    // Use this for initialization
    void Start () {
        UnlockText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnClick()
    {
        UnlockText.SetActive(false);
        UnlockText.SetActive(true);
    }
}
