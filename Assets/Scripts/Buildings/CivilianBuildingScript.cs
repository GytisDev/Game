using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianBuildingScript : MonoBehaviour {

    public int hasSpace = 2;

    PopulationManager popc;
    bool added;
    bool statisticAdded;
    ObjectOnGrid oog;
    ResourceManager rm;

    // Use this for initialization
    void Start () {
        added = false;
        statisticAdded = false;
        popc = FindObjectOfType<PopulationManager>();
        oog = gameObject.GetComponent<ObjectOnGrid>();
        rm = gameObject.GetComponent<ResourceManager>();

    }

    void Update()
    {
        if (!oog.placed) return;
        else
        {
            if (!statisticAdded) { StatisticsManager.CivilianBuildingCount++; statisticAdded = true; }
            AddSpace();
        }
        /*
        if (dc.iscold == true) 
        {
            cold += Time.deltaTime;
            if ((cold >= 5) && (rm.wood > 0))
            {
                rm.DecreaseResources(ResourceManager.Resources.Wood, 1);
                cold = 0;
            }
        }*/
    }

    void AddSpace()
    {
        if (added == false)
        {
            popc.IncreasePopulationLimit(hasSpace);
            added = true;
        }
    }
}
