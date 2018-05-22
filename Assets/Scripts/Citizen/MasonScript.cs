using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasonScript : MonoBehaviour {

    public enum States { GoingToWorkplace, Working, CarryingGoods, PathFinding, Available };
    public States state;
    ResourceManager rm;
    public MasonsHutController mhc;
    public GameObject citizen;
    public int Radius = 8;
    public int StoneAccumulation = 4;
    private int StoneGained;
    public float MiningTime = 8f;
    float currentMiningTime = 0f;
    public float FindingTime = 2f;
    float currentFindingTime = 0f;
    bool hasWorkToDo = false;
    bool noRocksInArea = false;
    GameObject currentRock;
    Unit unit;
    SoundManager sm;

    Vector3 lastPos, currentPos;

    // Use this for initialization
    void Start()
    {
        rm = FindObjectOfType<ResourceManager>();
        lastPos = gameObject.transform.position;
        unit = GetComponent<Unit>();
        sm = FindObjectOfType<SoundManager>();

    }

    private void OnDestroy() {
        mhc.ResetWork();

        if (currentRock != null) {
            currentRock.GetComponent<RockScript>().available = true;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (citizen == null) return;

        switch (state)
        {
            case States.GoingToWorkplace:
                if (ArrivedAtTarget(mhc.InitialPosition))
                {
                    state = States.Available;
                }
                break;

            case States.Available:
                if (hasWorkToDo) return;
                if (!noRocksInArea)
                    Work();
                else
                {
                    if (currentFindingTime < FindingTime)
                        currentFindingTime += Time.deltaTime;
                    else
                    {
                        currentFindingTime = 0f;
                        Work();
                    }
                }
                break;

            case States.PathFinding:
                if (currentRock == null)
                    ResetWork();

                if (ArrivedAtTarget(currentRock))
                {
                    AudioSource audio = currentRock.GetComponent<AudioSource>();
                    audio.volume = sm.volume;
                    audio.Play();
                    state = States.Working;
                    //Debug.Log("Arived at the rock");
                }
                break;

            case States.Working:
                if (currentRock == null)
                    ResetWork();

                if (currentMiningTime < MiningTime)
                {
                    currentMiningTime += Time.deltaTime;
                }
                else
                {
                    RockScript ts = currentRock.GetComponent<RockScript>();
                    StoneGained = ts.Mine(StoneAccumulation);
                    ts.CutDownIfEmpty();
                    RockCutted();
                }
                break;

            case States.CarryingGoods:
                if (ArrivedAtTarget(mhc.InitialPosition))
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

    public void RockCutted()
    {
        ObjectOnGrid oog = currentRock.GetComponent<ObjectOnGrid>();
        oog.grid.SetWalkable(oog.gridPosX, oog.gridPosY);

        currentRock = null;
        currentMiningTime = 0f;
        state = States.CarryingGoods;

        unit.MoveTo(mhc.InitialPosition.transform.position);
    }

    public bool ArrivedAtTarget(GameObject targetObject)
    {
        return unit.ArrivedAtTarget(targetObject.transform);
    }

    public void Work()
    {
        if (rm.stone < rm.storage)
        {
            FindNearestRock();

            if (currentRock == null)
            {
                noRocksInArea = true;
                return;
            }

            noRocksInArea = false;
            unit.MoveTo(currentRock.transform.position);
            hasWorkToDo = true;
            state = States.PathFinding;
        }
    }

    void FindRock()
    {
        currentRock = null;
        Grid grid = FindObjectOfType<Grid>();
        Node HutNode = grid.NodeFromWorldPoint(transform.position);
        foreach (GameObject rock in GameObject.FindGameObjectsWithTag("Rock"))
        {
            Node rockNode = grid.NodeFromWorldPoint(rock.transform.position);

            if (rockNode.gridX <= HutNode.gridX + Radius &&
                rockNode.gridX >= HutNode.gridX - Radius &&
                rockNode.gridY <= HutNode.gridY + Radius &&
                rockNode.gridY >= HutNode.gridY - Radius)

            {
                RockScript rs = rock.GetComponent<RockScript>();
                ObjectOnGrid oog = rock.GetComponent<ObjectOnGrid>();
                if (rs.available && oog.placed)
                {
                    currentRock = rock;
                    rs.available = false;
                    return;
                }
            }
        }
    }

    void FindNearestRock()
    {
        currentRock = null;
        float dist = float.MaxValue;
        Grid grid = FindObjectOfType<Grid>();
        Node HutNode = grid.NodeFromWorldPoint(transform.position);
        foreach (GameObject rock in GameObject.FindGameObjectsWithTag("Rock"))
        {
            Node rockNode = grid.NodeFromWorldPoint(rock.transform.position);

            if (rockNode.gridX <= HutNode.gridX + Radius &&
                rockNode.gridX >= HutNode.gridX - Radius &&
                rockNode.gridY <= HutNode.gridY + Radius &&
                rockNode.gridY >= HutNode.gridY - Radius)

            {
                RockScript rs = rock.GetComponent<RockScript>();
                ObjectOnGrid oog = rock.GetComponent<ObjectOnGrid>();
                if (rs.available && oog.placed)
                {
                    float currRockDist = Mathf.Sqrt(Mathf.Pow(rockNode.gridX - HutNode.gridX, 2) +
                                                  Mathf.Pow(rockNode.gridY - HutNode.gridY, 2));
                    if (currRockDist < dist)
                    {
                        currentRock = rock;
                        dist = currRockDist;
                    }
                }
            }
        }

        if (currentRock != null)
        {
            RockScript rs = currentRock.GetComponent<RockScript>();
            rs.available = false;
        }
    }

    void GoodsArrived()
    {
        // Here you can update resourses using
        // WoodGained - wood gained from last trip
        //Debug.Log("Goods arrived");

        rm.IncreaseResources(ResourceManager.Resources.Stone, StoneGained);

    }

    public void ResetWork()
    {
        hasWorkToDo = false;
        state = States.GoingToWorkplace;
        unit.MoveTo(mhc.InitialPosition.transform.position);
    }

    public void ChangeState_GoingToWorkplace()
    {
        state = States.GoingToWorkplace;
    }
}
