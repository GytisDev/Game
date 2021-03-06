﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherersHutController : MonoBehaviour {

    public GameObject InitialPosition;      // The position where citizen should initially come at work
    private bool citizenAsigned = false;
    GameObject AsignedCitizen = null;
    ObjectOnGrid oog;
    bool statisticAdded;

    // Use this for initialization
    void Start () {
        oog = gameObject.GetComponent<ObjectOnGrid>();
        statisticAdded = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!oog.placed) return;

        if (!statisticAdded) { StatisticsManager.GatherersHutCount++; statisticAdded = true; }

        if (!citizenAsigned)
        {
            AsignCitizens();
        }
    }

    public void ResetWork() {
        citizenAsigned = false;
        AsignedCitizen = null;
    }

    public void AsignCitizens()
    {
        foreach (GameObject citizen in GameObject.FindGameObjectsWithTag("Citizen"))
        {
            if (!citizenAsigned)
            {
                CitizenScript citizenScript = citizen.GetComponent<CitizenScript>();
                if (citizenScript.available && citizenScript.profession == CitizenScript.Professions.Unemployeed)
                {
                    citizenAsigned = true;

                    citizenScript.available = false;
                    citizenScript.profession = CitizenScript.Professions.Gatherer;

                    // Add WoodCutterScript componenet to asigned citizen
                    citizen.AddComponent<GatherersScript>();
                    GatherersScript worker = citizen.GetComponent<GatherersScript>();
                    worker.citizen = citizen;
                    System.Console.WriteLine("Found: " + worker.citizen.ToString());
                    worker.hut = this;
                    worker.GoToWorkplace();
                }
            }
        }
    }
}
