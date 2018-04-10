using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherersScript : MonoBehaviour {

    public enum States { GoingToWorkplace, Working, CarryingGoods, PathFinding, Available };
    public States state;
    ResourceManager rm;
    public GatherersHutController hut;
    public GameObject citizen;
    public int Radius = 30;
    public float GatheringTime = 1f;
    public int FoodAccumulation = 40;
    private int FoodGained;
    float currentGatheringTime = 0;
    bool hasWorkToDo = false;
    GameObject currentBush;
    Unit unit;

    Vector3 lastPos, currentPos;

    // Use this for initialization
    void Start()
    {
        rm = FindObjectOfType<ResourceManager>();
        unit = GetComponent<Unit>();
        lastPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update () {
        switch (state)
        {
            case States.GoingToWorkplace:
                if (ArrivedAtTarget(hut.InitialPosition))
                {
                    state = States.Available;
                }
                break;

            case States.Available:
                if (!hasWorkToDo)
                {
                    Work();
                }
                else
                {

                }
                break;

            case States.PathFinding:
                if (currentBush == null)
                    ResetWork();

                if (ArrivedAtTarget(currentBush))
                {
                    state = States.Working;
                    Debug.Log("Arived at the bush");
                }
                break;

            case States.Working:
                if (currentBush == null)
                    ResetWork();

                if (currentGatheringTime < GatheringTime)
                {
                    currentGatheringTime += Time.deltaTime;
                }
                else
                {
                    BerrieBushScript ts = currentBush.GetComponent<BerrieBushScript>();
                    FoodGained = ts.Collect(FoodAccumulation);
                    BerriesCollected();
                }
                break;

            case States.CarryingGoods:
                if (ArrivedAtTarget(hut.InitialPosition))
                {
                    GoodsArrived();
                    state = States.Available;
                    hasWorkToDo = false;
                }
                break;

            default:

                break;
        }
    }

    public void Work()
    {
        if (rm.food < rm.storage)
        {
            FindNearestBush();

            if (currentBush == null) return;

            Debug.Log("Bush found");

            //Unit unit = citizen.GetComponent<Unit>();
            unit.MoveTo(currentBush.transform.position);
            hasWorkToDo = true;
            state = States.PathFinding;
        }
    }

    void FindNearestBush()
    {
        currentBush = null;
        float dist = float.MaxValue;
        Grid grid = FindObjectOfType<Grid>();
        Node HutNode = grid.NodeFromWorldPoint(transform.position);
        foreach (GameObject bush in GameObject.FindGameObjectsWithTag("BerryBush"))
        {
            Node bushNode = grid.NodeFromWorldPoint(bush.transform.position);

            if (bushNode.gridX <= HutNode.gridX + Radius &&
                bushNode.gridX >= HutNode.gridX - Radius &&
                bushNode.gridY <= HutNode.gridY + Radius &&
                bushNode.gridY >= HutNode.gridY - Radius)

            {
                BerrieBushScript cs = bush.GetComponent<BerrieBushScript>();
                ObjectOnGrid oog = bush.GetComponent<ObjectOnGrid>();
                if (cs.available && oog.placed)
                {
                    float currBushDist = Mathf.Sqrt(Mathf.Pow(bushNode.gridX - HutNode.gridX, 2) +
                                                  Mathf.Pow(bushNode.gridY - HutNode.gridY, 2));
                    if (currBushDist < dist)
                    {
                        currentBush = bush;
                        dist = currBushDist;
                    }
                    //ts.available = false;
                    //return;
                }
            }
        }

        if (currentBush != null)
        {
            BerrieBushScript cs = currentBush.GetComponent<BerrieBushScript>();
            cs.available = false;
        }
    }

    public void BerriesCollected()
    {
        currentBush = null;
        currentGatheringTime = 0f;
        state = States.CarryingGoods;

        //Unit unit = citizen.GetComponent<Unit>();
        unit.MoveTo(hut.InitialPosition.transform.position);
    }

    void GoodsArrived()
    {
        // Here you can update resourses using
        // FoodGained - food gained from last trip
        Debug.Log("Goods arrived");

        rm.IncreaseResources(ResourceManager.Resources.Food, FoodGained);
    }

    public void ResetWork()
    {
        hasWorkToDo = false;
        state = States.GoingToWorkplace;
        Unit unit = citizen.GetComponent<Unit>();
        unit.MoveTo(hut.InitialPosition.transform.position);
    }

    public void GoToWorkplace()
    {
        state = States.GoingToWorkplace;
        Unit unit = citizen.GetComponent<Unit>();
        unit.MoveTo(hut.InitialPosition.transform.position);
    }

    public bool ArrivedAtTarget(GameObject targetObject)
    {
        return citizen.GetComponent<Unit>().ArrivedAtTarget(targetObject.transform);
    }
}
