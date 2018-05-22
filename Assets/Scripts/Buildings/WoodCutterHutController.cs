using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterHutController : MonoBehaviour {

    public GameObject InitialPosition;      // The position where citizen should initially come at work
    private bool citizenAsigned = false;
    GameObject AsignedCitizen = null;
    ObjectOnGrid oog;
    bool statisticAdded;

    private void Start() {
        oog = gameObject.GetComponent<ObjectOnGrid>();
        statisticAdded = false;
    }

    // Update is called once per frame
    void Update () {
        if (!oog.placed) return;

        if (!statisticAdded) { StatisticsManager.WoodCutterHutCount++; statisticAdded = true; }

        if (!citizenAsigned)
        {
            AsignCitizens();
        } else {
            if(AsignedCitizen == null) {
                ResetWork();
            }
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

                    // Add WoodCutterScript componenet to asigned citizen
                    citizen.AddComponent<WoodCutterScript>();
                    WoodCutterScript wcs = citizen.GetComponent<WoodCutterScript>();
                    wcs.citizen = citizen;
                    wcs.wcc = this;
                    wcs.ChangeState_GoingToWorkplace();

                    // Citizen goes to his workplace
                    Unit unit = citizen.GetComponent<Unit>();
                    unit.MoveTo(InitialPosition.transform.position);
                    AsignedCitizen = citizen;
                }
            }
        }
    }
}
