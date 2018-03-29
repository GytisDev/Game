using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : MonoBehaviour {

    public GameObject green, yellow, red;
    PopulationManager popc;
    ResourceManager rm;
    int happiness;
	// Use this for initialization
	void Start () {
        popc = FindObjectOfType<PopulationManager>();
        rm = FindObjectOfType<ResourceManager>();
    }
	
	// Update is called once per frame
	void Update () {
        CalculateHappiness();
        if (happiness > 75)
        {
            green.SetActive(true);
            yellow.SetActive(false);
            red.SetActive(false);
        }
        else if (happiness < 25)
        {
            green.SetActive(false);
            yellow.SetActive(false);
            red.SetActive(true);
        }
        else
        {
            green.SetActive(false);
            yellow.SetActive(true);
            red.SetActive(false);
        }
    }

    void CalculateHappiness()
    {
        happiness = 50;
        if (popc.popLimit - popc.population > popc.population) happiness += 30;
        if (popc.popLimit - popc.population < popc.population / 5) happiness -= 30;
        if (rm.food == 0) happiness -= 60;

    }
}
