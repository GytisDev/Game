using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : MonoBehaviour {

    public GameObject green, yellow, red;
    PopulationManager popc;
    ResourceManager rm;
    public float famineKillRate;
    float killTimer;
    public int happiness;
	// Use this for initialization
	void Start () {
        popc = FindObjectOfType<PopulationManager>();
        rm = FindObjectOfType<ResourceManager>();
        killTimer = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        CalculateHappiness();
        if (happiness > 75)
        {
            green.SetActive(true);
            yellow.SetActive(false);
            red.SetActive(false);
            killTimer = 0f;
        }
        else if (happiness < 25)
        {
            green.SetActive(false);
            yellow.SetActive(false);
            red.SetActive(true);

            if (killTimer > famineKillRate) {
                KillRandom();
                killTimer = 0f;
            } else {
                killTimer += Time.deltaTime;
            }

        }
        else
        {
            green.SetActive(false);
            yellow.SetActive(true);
            red.SetActive(false);
            killTimer = 0f;
        }
    }

    void CalculateHappiness()
    {
        happiness = 50;
        if (popc.popLimit - popc.population >= 0) happiness += 30;
        if (popc.population > popc.popLimit) happiness -= 30;
        if (rm.food == 0) happiness -= 60;

    }

    void KillRandom() {
        CitizenScript citizenScript;
        //int professionsChecked = 0;

        GameObject[] citizens = GameObject.FindGameObjectsWithTag("Citizen");

        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < citizens.Length; j++) {
                citizenScript = citizens[j].GetComponent<CitizenScript>();
                CitizenScript.Professions prof = citizenScript.profession;

                switch (i) {
                    case 0:
                        if (prof == CitizenScript.Professions.Unemployeed) {
                            Destroy(citizens[j]);
                            return;
                        }
                        break;
                    case 1:
                        if (prof == CitizenScript.Professions.Mason) {
                            Destroy(citizens[j]);
                            return;
                        }
                        break;
                    case 2:
                        if (prof == CitizenScript.Professions.Forester) {
                            Destroy(citizens[j]);
                            return;
                        }
                        break;
                    case 3:
                        if (prof == CitizenScript.Professions.WoodCutter) {
                            Destroy(citizens[j]);
                            return;
                        }
                        break;
                    default:

                        break;
                }
            }
        }

        //foreach (GameObject citizen in GameObject.FindGameObjectsWithTag("Citizen")) {
        //    citizenScript = citizen.GetComponent<CitizenScript>();
        //    CitizenScript.Professions prof = citizenScript.profession;

        //    switch (professionsChecked) {
        //        case 0:
        //            if (prof == CitizenScript.Professions.Unemployeed) {
        //                Destroy(citizen);
        //                return;
        //            }
        //            break;
        //        case 1:
        //            if (prof == CitizenScript.Professions.Mason) {
        //                Destroy(citizen);
        //                return;
        //            }
        //            break;
        //        case 2:
        //            if (prof == CitizenScript.Professions.Forester) {
        //                Destroy(citizen);
        //                return;
        //            }
        //            break;
        //        case 3:
        //            if (prof == CitizenScript.Professions.WoodCutter) {
        //                Destroy(citizen);
        //                return;
        //            }
        //            break;
        //        default:

        //            break;
        //    }
        //}
    }
}
