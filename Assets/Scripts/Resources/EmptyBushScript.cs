﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBushScript : MonoBehaviour {

    public float growthTime = 10f;
    public float currTime = 0;
    public GameObject BerryBush;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currTime >= growthTime)
        {
            GameObject berryBush = Instantiate(BerryBush);
            berryBush.transform.position = gameObject.transform.position;
            berryBush.transform.rotation = gameObject.transform.rotation;
            ObjectOnGrid oogBerryBush = berryBush.GetComponent<ObjectOnGrid>();
            oogBerryBush.placed = true;
            oogBerryBush.SetNodes(GetComponent<ObjectOnGrid>().Nodes);
            Destroy(gameObject);
        }
        else
        {
            currTime += Time.deltaTime;
        }
    }
}
