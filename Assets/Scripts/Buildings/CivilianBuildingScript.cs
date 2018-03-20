using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianBuildingScript : MonoBehaviour {

    PopulationManager popc;
    bool added;

	// Use this for initialization
	void Start () {
        added = false;
        popc = FindObjectOfType<PopulationManager>();
    }

    void Update()
    {
        ObjectOnGrid oog = gameObject.GetComponent<ObjectOnGrid>();
        if (!oog.placed) return;
        else
        {
            AddSpace();
        }
    }

    void AddSpace()
    {
        if (added == false)
        {
            popc.IncreasePopulationLimit(5);
            added = true;
        }
    }
}
