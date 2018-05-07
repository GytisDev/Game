using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianBuildingScript : MonoBehaviour {

    public int hasSpace = 2;

    PopulationManager popc;
    bool added;
    bool statisticAdded;
    ObjectOnGrid oog;

    // Use this for initialization
    void Start () {
        added = false;
        statisticAdded = false;
        popc = FindObjectOfType<PopulationManager>();
        oog = gameObject.GetComponent<ObjectOnGrid>();
    }

    void Update()
    {
        if (!oog.placed) return;
        else
        {
            if (!statisticAdded) { StatisticsManager.CivilianBuildingCount++; statisticAdded = true; }
            AddSpace();
        }
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
