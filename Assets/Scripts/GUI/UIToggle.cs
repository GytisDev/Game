using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIToggle : MonoBehaviour
{
    public bool AllToggle;
    bool isMMOn;
    public GameObject Minimap;
    bool isRstOn;
    public GameObject ResourceTab;

    void Start()
    {
        Minimap.SetActive(AllToggle);
        isMMOn = AllToggle;
        ResourceTab.SetActive(AllToggle);
        isRstOn = AllToggle;
    }

    void Update()
    {
        ResourceToggle();
        MinimapToggle();
    }
    void ResourceToggle()
    {
        if (Input.GetKeyDown("u"))
        {

            if (isRstOn == true)
            {
                ResourceTab.SetActive(false);
                isRstOn = false;
            }

            else
            {
                ResourceTab.SetActive(true);
                isRstOn = true;
            }
        }
    }

    void MinimapToggle()
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