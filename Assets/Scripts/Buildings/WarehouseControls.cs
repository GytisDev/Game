using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseControls : MonoBehaviour {

    ResourceManager rm;
    bool added = false;
    ObjectOnGrid oog;
    bool statisticAdded;

    // Use this for initialization
    void Start () {
        rm = FindObjectOfType<ResourceManager>();
        oog = gameObject.GetComponent<ObjectOnGrid>();
        statisticAdded = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!oog.placed) return;
        else
        {
            if (!statisticAdded) { StatisticsManager.WarehouseCount++; statisticAdded = true; }
            AddStorage();
        }
    }

    void AddStorage()
    {
        if (added == false)
        {
            rm.IncreaseResources(ResourceManager.Resources.Storage, 800);
            added = true;
        }
    }
}
