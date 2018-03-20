using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationManager : MonoBehaviour {

    public Text poptext;
    public int Population;
    public int poplimit;

	// Use this for initialization
	void Start () {
        Population = 0;
        poplimit = 5;
	}
	
	// Update is called once per frame
	void Update () {
        poptext.text = Population.ToString() + "\n/" + poplimit.ToString();
	}

    public void DecreasePopulation(int i)
    {
        Population -= i;
    }

    public void IncreasePopulation(int i)
    {
        Population += i;
    }

    public void DecreasePopulationLimit(int i)
    {
        poplimit -= i;
    }

    public void IncreasePopulationLimit(int i)
    {
        poplimit += i;
    }
}
