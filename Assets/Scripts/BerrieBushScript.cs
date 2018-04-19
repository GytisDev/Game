using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerrieBushScript : MonoBehaviour {

    public int baseFood;
    public int foodLeft;
    public bool available;
    public float growthTime = 10f;
    public float currTime = 0;
    public GameObject BushWithoutBerries;
    // Use this for initialization
    void Start()
    {
        available = true;
    }
	
	// Update is called once per frame
	void Update () {
		if(foodLeft == 0)
        {
            GameObject bushWithoutBerries = Instantiate(BushWithoutBerries);
            bushWithoutBerries.transform.position = gameObject.transform.position;
            bushWithoutBerries.transform.rotation = gameObject.transform.rotation;

            Destroy(gameObject);
        }
	}

    public int Collect(int quantity)
    {
        available = true;
        if (quantity > foodLeft)
            quantity = foodLeft;
        foodLeft -= quantity;

        if (foodLeft == 0)
            available = false;
        else
            available = true;
 
        return quantity;
    }
}
