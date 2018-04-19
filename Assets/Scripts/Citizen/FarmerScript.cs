using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerScript : MonoBehaviour {

    public enum States { GoingToWorkplace, NeedsCultivating, GoingToCultivate,
        Cultivating, NeedsPlanting, GoingToPlant, NeedsCutting, GoingToCut, Cutting,
        BringingFood, Planting, CommingBack, PathFinding, Available };

    public States state;
    public FarmingShackController fsc;
    public GameObject citizen;
    public int Radius;
    public float plantingTime = 0f;
    public float cultivatingTime = 0f;
    public float cuttingTime = 0f;
    public float timerToWork = 0;
    FieldScript currentField;

    ResourceManager rm;
    float currentCultivatingTime = 0f;
    float currentPlantingTime = 0f;
    float currentCuttingTime = 0f;
    int carryingFood = 0;
    GameObject currentFieldLocation;
    Node targetNode = null;
    Unit unit;
    Grid grid;
    List<FieldScript> fields;
    Vector3 targetPosition;

    Vector3 lastPos, currentPos;

    // Use this for initialization
    void Start() {
        rm = FindObjectOfType<ResourceManager>();
        lastPos = gameObject.transform.position;
        unit = GetComponent<Unit>();
        Radius = fsc.Radius;
        grid = FindObjectOfType<Grid>();
        fields = fsc.Fields;
        state = States.Available;
    }

    // Update is called once per frame
    void Update() {
        if (citizen == null) return;

        timerToWork += Time.deltaTime;

        if (timerToWork >= 1 && state == States.Available) {
            if (isThereUncultivated()) {
                state = States.NeedsCultivating;
            }
            else if (isThereUncut() && rm.food + 10 < rm.storage) {
                state = States.NeedsCutting;
            }
            else if (isThereUnplanted()) {
                state = States.NeedsPlanting;
            }
            
            timerToWork = 0;
        }

        switch (state) {
            case States.NeedsCultivating:
                currentField = getUncultivatedFields()[0];
                if (currentField == null) {
                    state = States.CommingBack;
                    unit.MoveTo(fsc.InitialPosition.transform.position);
                }
                else {
                    unit.MoveTo(currentField.transform.position);
                    state = States.GoingToCultivate;
                }
                break;

            case States.NeedsPlanting:
                currentField = getUnplantedFields()[0];
                if (currentField == null) {
                    state = States.CommingBack;
                    unit.MoveTo(fsc.InitialPosition.transform.position);
                }
                else {
                    unit.MoveTo(currentField.transform.position);
                    state = States.GoingToPlant;
                }
                break;
            case States.GoingToPlant:
                if (ArrivedAtTarget()) {
                    state = States.Planting;
                }
                break;

            case States.Planting:
                if (currentPlantingTime < plantingTime) {
                    currentPlantingTime += Time.deltaTime;
                }
                else {
                    currentField.SetPlanted();
                    unit.MoveTo(fsc.InitialPosition.transform.position);
                    state = States.CommingBack;
                    currentPlantingTime = 0;
                }
                break;

            case States.GoingToCultivate:
                if (ArrivedAtTarget()) {
                    state = States.Cultivating;
                }
                break;
            case States.Cultivating:
                if(currentCultivatingTime < cultivatingTime) {
                    currentCultivatingTime += Time.deltaTime;
                }
                else {
                    currentField.SetCultivated();
                    unit.MoveTo(fsc.InitialPosition.transform.position);
                    state = States.CommingBack;
                    currentCultivatingTime = 0;
                }
                break;
            case States.CommingBack:
                if (ArrivedAtTarget()) {
                    state = States.Available;
                    rm.IncreaseResources(ResourceManager.Resources.Food, carryingFood);
                    carryingFood = 0;
                }
                break;
            case States.NeedsCutting:
                currentField = getUncutFields()[0];
                if (currentField == null) {
                    state = States.CommingBack;
                    unit.MoveTo(fsc.InitialPosition.transform.position);
                }
                else {
                    unit.MoveTo(currentField.transform.position);
                    state = States.GoingToCut;
                }
                break;
            case States.GoingToCut:
                if (ArrivedAtTarget()) {
                    state = States.Cutting;
                }
                break;
            case States.Cutting:
                if (currentCuttingTime < cuttingTime) {
                    currentCuttingTime += Time.deltaTime;
                }
                else {
                    currentField.SetCut();
                    unit.MoveTo(fsc.InitialPosition.transform.position);
                    state = States.CommingBack;
                    currentCultivatingTime = 0;
                    carryingFood += 10;
                }
                break;
            default:

                break;
        }
    }

    List<FieldScript> getUncutFields() {
        List<FieldScript> list = new List<FieldScript>();

        if (fields.Count <= 0)
            return list;

        foreach (FieldScript field in fields) {
            if (field.stage == FieldScript.Stage.Grown) {
                list.Add(field);
            }
        }

        return list;
    }

    bool isThereUncut() {
        return getUncutFields().Count > 0;
    }

    List<FieldScript> getUnplantedFields() {
        List<FieldScript> list = new List<FieldScript>();

        if (fields.Count <= 0)
            return list;

        foreach (FieldScript field in fields) {
            if (field.stage == FieldScript.Stage.Empty) {
                list.Add(field);
            }
        }

        return list;
    }

    bool isThereUnplanted() {
        List<FieldScript> unplantedFields = getUnplantedFields();
        return getUnplantedFields().Count > 0;
    }

    List<FieldScript> getUncultivatedFields() {
        List<FieldScript> list = new List<FieldScript>();
        if (fields.Count <= 0)
            return list;

        foreach (FieldScript field in fields) {
            if (field.stage == FieldScript.Stage.None) {
                list.Add(field);
            }
        }

        return list;
    }

    public void ChangeState_GoingToWork() {
        state = States.CommingBack;
    }

    bool isThereUncultivated() {
        return getUncultivatedFields().Count > 0;
    }

    public bool ArrivedAtTarget() {
        return unit.ArrivedAtTarget();
    }
}
