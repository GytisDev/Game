using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingOnClick : MonoBehaviour {

    public GameObject info;
    public Text infobar;
    public string message;
    public GameObject building;
    private BuildingPlacement buildingPlacement;
    ResourceManager rm;

    // Use this for initialization
    void Start()
    {
        buildingPlacement = GetComponent<BuildingPlacement>();
        rm = FindObjectOfType<ResourceManager>();
        message += "\n";
        ObjectOnGrid ong = building.GetComponent<ObjectOnGrid>();
        if (ong.costWood != 0)
        {
            message += ong.costWood + " wood ";
        }
        if (ong.costStone != 0)
        {
            message += ong.costStone + " stone ";
        }
        if (ong.costStone == 0 && ong.costWood == 0)
        {
            message += "FREE";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HoverEnter()
    {
        info.SetActive(true);
        infobar.text = message;
    }

    public void HoverExit()
    {
        info.SetActive(false);
        infobar.text = "";
    }

    public void TaskOnClick()
    {
        buildingPlacement.currentBuilding = null;

        ObjectOnGrid ong = building.GetComponent<ObjectOnGrid>();

        if (rm.isEnough(ong.costWood, ong.costStone, 0))
            buildingPlacement.SetItem(building);
    }
}
