using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationManager : MonoBehaviour {

    public Text poptext;
    public int population;
    public int popLimit;

	// Use this for initialization
	void Start () {
        population = 0;
        popLimit = 5;
	}
	
	// Update is called once per frame
	void Update () {
        poptext.text = population.ToString() + "\n/" + popLimit.ToString();
	}

    public void DecreasePopulation(int i)
    {
        population -= i;
        StatisticsManager.CivilianCount -= i;
    }

    public void IncreasePopulation(int i)
    {
        population += i;
        StatisticsManager.CivilianCount += i;
    }

    public void DecreasePopulationLimit(int i)
    {
        popLimit -= i;
    }

    public void IncreasePopulationLimit(int i)
    {
        popLimit += i;
    }

    public bool isPopLimitReached() {
        return population >= popLimit;
    }

    public bool isPopLimitReached(int count) {
        return population + count >= popLimit;
    }
}
