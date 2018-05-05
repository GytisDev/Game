using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasonsHutController : MonoBehaviour {

    public GameObject InitialPosition;      // The position where citizen should initially come at work
    private bool citizenAsigned = false;
    GameObject[] AsignedCitizens = new GameObject[1];
    ObjectOnGrid oog;

    private void Start() {
        oog = gameObject.GetComponent<ObjectOnGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!oog.placed) return;

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
                    citizen.AddComponent<MasonScript>();
                    MasonScript ms = citizen.GetComponent<MasonScript>();
                    ms.citizen = citizen;
                    ms.mhc = this;
                    ms.ChangeState_GoingToWorkplace();

                    // Citizen goes to his workplace
                    Unit unit = citizen.GetComponent<Unit>();
                    unit.MoveTo(InitialPosition.transform.position);
                }
            }
        }
    }
}
