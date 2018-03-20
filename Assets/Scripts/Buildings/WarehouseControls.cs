using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseControls : MonoBehaviour {

    ResourceManager rm;
    bool added = false;

	// Use this for initialization
	void Start () {
        rm = FindObjectOfType<ResourceManager>();

    }
	
	// Update is called once per frame
	void Update () {
        ObjectOnGrid oog = gameObject.GetComponent<ObjectOnGrid>();
        if (!oog.placed) return;
        else
        {
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
