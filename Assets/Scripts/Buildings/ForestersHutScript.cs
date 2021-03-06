﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestersHutScript : MonoBehaviour {

    public GameObject InitialPosition;      // The position where citizen should initially come at work
    public GameObject YoungTree;
    public int Radius = 12;
    private bool citizenAsigned = false;
    GameObject AsignedCitizen = null;
    public ObjectOnGrid oog;
    bool statisticAdded;

    private void Start() {
        oog = gameObject.GetComponent<ObjectOnGrid>();
        statisticAdded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!oog.placed) return;

        if (!statisticAdded) { StatisticsManager.ForestersHutCount++; statisticAdded = true; }

        if (!citizenAsigned) {
            AsignCitizens();
        }
        //else {
        //    if (AsignedCitizen == null) {
        //        ResetWork();
        //    }
        //}
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
                    citizenScript.profession = CitizenScript.Professions.Forester;
                    // Add ForesterScript componenet to asigned citizen
                    citizen.AddComponent<ForesterScript>();
                    ForesterScript fs = citizen.GetComponent<ForesterScript>();
                    fs.citizen = citizen;
                    fs.fhs = this;
                    fs.ChangeState_GoingToWorkplace();

                    // Citizen goes to his workplace
                    Unit unit = citizen.GetComponent<Unit>();
                    unit.MoveTo(InitialPosition.transform.position);
                }
            }
        }
    }
}
