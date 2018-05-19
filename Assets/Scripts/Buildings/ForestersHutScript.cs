using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestersHutScript : MonoBehaviour {

    public GameObject InitialPosition;      // The position where citizen should initially come at work
    public GameObject YoungTree;
    public int Radius = 12;
    private bool citizenAsigned = false;
    GameObject[] AsignedCitizens = new GameObject[1];
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
