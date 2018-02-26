using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MinimapToggle : MonoBehaviour
{

    public bool isMMOn;
    public GameObject Minimap;

    void Start()
    {
        Minimap.SetActive(true);
        isMMOn = true;
    }

    void Update()
    {

        if (Input.GetKeyDown("m"))
        {

            if (isMMOn == true)
            {
                Minimap.SetActive(false);
                isMMOn = false;
            }

            else
            {
                Minimap.SetActive(true);
                isMMOn = true;
            }
        }
    }
}