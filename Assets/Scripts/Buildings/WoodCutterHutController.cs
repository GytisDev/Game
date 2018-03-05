using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterHutController : MonoBehaviour {

    public GameObject InitialPosition;      // The position where citizen should initially come at work
    private bool citizenAsigned = false;
    GameObject[] AsignedCitizens = new GameObject[1];

	
	// Update is called once per frame
	void Update () {
        if (!citizenAsigned)
        {
            AsignCitizens();
        }
	}

    public void AsignCitizens()
    {
        foreach (GameObject citizen in GameObject.FindGameObjectsWithTag("Citizen"))
        {
            if (!citizenAsigned)
            {
                CitizenScript citizenScript = citizen.GetComponent<CitizenScript>();
                if (citizenScript.available)
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
                    unit.target = InitialPosition.transform;
                }
            }
        }
    }
}
