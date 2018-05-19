using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenScript : MonoBehaviour {

    public bool available;
    PopulationManager popc;
    ResourceManager rm;
    float age;
    float hunger;
    int gluttony;

    // Use this for initialization
    void Start () {
        gluttony = Random.Range(5, 10);
        age = 0;
        hunger = 0;
        popc = FindObjectOfType <PopulationManager>();
        rm = FindObjectOfType<ResourceManager>();
        available = true;
        popc.IncreasePopulation(1);
	}
	
	// Update is called once per frame
	void Update () {
        age += Time.deltaTime;
        hunger += Time.deltaTime;
        if (hunger > gluttony)
        {
            if (rm.food > 0)
            {
                rm.DecreaseResources(ResourceManager.Resources.Food, 1);
                hunger = 0;
            }
            //else Destroy(this);
        }
        if (age > 300) Destroy(this);
    }


}
