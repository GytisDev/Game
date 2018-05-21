using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenScript : MonoBehaviour {

    public enum Professions { WoodCutter, Mason, Gatherer, Farmer, Forester, Unemployeed};
    public bool available;
    PopulationManager popc;
    ResourceManager rm;
    Daycount daycount;
    float age;
    float hunger;
    int gluttony;
    public Professions profession;

    // Use this for initialization
    void Start () {
        daycount = FindObjectOfType<Daycount>();
        gluttony = Random.Range(5, 10);
        age = 0;
        hunger = 0;
        popc = FindObjectOfType <PopulationManager>();
        rm = FindObjectOfType<ResourceManager>();
        available = true;
        popc.IncreasePopulation(1);
	}

    public void OnDestroy() {
        popc.DecreasePopulation(1);
        StatisticsManager.CivilianCount--;
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
