using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterScript : MonoBehaviour {

    public enum States {GoingToWorkplace, Working, CarryingGoods, PathFinding, Available };
    public States state;
    ResourceManager rm;
    public WoodCutterHutController wcc;
    public GameObject citizen;
    public int Radius = 30;
    public float ChopingTime = 1f;
    public int WoodAccumulation = 40;
    private int WoodGained;
    float currentChopingTime = 0;
    bool hasWorkToDo = false;
    GameObject currenTree;

    Vector3 lastPos, currentPos;

    // Use this for initialization
    void Start () {
        rm = FindObjectOfType<ResourceManager>();
        lastPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {


        if (citizen == null) return;

        switch (state)
        {
            case States.GoingToWorkplace:
                if (ArrivedAtTarget(wcc.InitialPosition))
                {
                    state = States.Available;
                }
                break;

            case States.Available:
                if (!hasWorkToDo)
                {
                    Work();
                } else
                {

                }
                break;

            case States.PathFinding:
                if (currenTree == null)
                    ResetWork();

                if (ArrivedAtTarget(currenTree))
                {
                    state = States.Working;
                    Debug.Log("Arived at tree");
                }
                else
                {
                    //Debug.Log("Going");
                }
                break;

            case States.Working:
                if (currenTree == null)
                    ResetWork();

                if (currentChopingTime < ChopingTime)
                {
                    currentChopingTime += Time.deltaTime;
                } else
                {
                    TreeScript ts = currenTree.GetComponent<TreeScript>();
                    WoodGained = ts.Chop(WoodAccumulation);
                    ts.ChopDownIfEmpty();
                    TreeChoped();
                }
                break;

            case States.CarryingGoods:
                if (ArrivedAtTarget(wcc.InitialPosition))
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

    public void TreeChoped()
    {
        currenTree = null;
        currentChopingTime = 0f;
        state = States.CarryingGoods;

        Unit unit = citizen.GetComponent<Unit>();
        unit.target = wcc.InitialPosition.transform;
    }

    public bool ArrivedAtTarget(GameObject targetObject)
    {
        return citizen.GetComponent<Unit>().ArivedAtTarget(targetObject);
    }

    public void Work()
    {
        FindTree();

        if (currenTree == null) return;

        Debug.Log("Tree found");

        Unit unit = citizen.GetComponent<Unit>();
        unit.target = currenTree.transform;
        hasWorkToDo = true;
        state = States.PathFinding;
    }

    void FindTree()
    {
        currenTree = null;
        Grid grid = FindObjectOfType<Grid>();
        Node HutNode = grid.NodeFromWorldPoint(transform.position);
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree"))
        {
            Node treeNode = grid.NodeFromWorldPoint(tree.transform.position);

            if (treeNode.gridX <= HutNode.gridX + Radius &&
                treeNode.gridX >= HutNode.gridX - Radius &&
                treeNode.gridY <= HutNode.gridY + Radius &&
                treeNode.gridY >= HutNode.gridY - Radius )

            {
                TreeScript ts = tree.GetComponent<TreeScript>();
                ObjectOnGrid oog = tree.GetComponent<ObjectOnGrid>();
                if (ts.available && oog.placed)
                {
                    currenTree = tree;
                    ts.available = false;
                    return;
                }
            }
        }
    }

    void GoodsArrived()
    {
        // Here you can update resourses using
        // WoodGained - wood gained from last trip
        Debug.Log("Goods arrived");

        rm.IncreaseResources(ResourceManager.Resources.Wood, WoodGained);

    }

    public void ResetWork()
    {
        hasWorkToDo = false;
        state = States.GoingToWorkplace;
        Unit unit = citizen.GetComponent<Unit>();
        unit.target = wcc.InitialPosition.transform;
    }

    public void ChangeState_GoingToWorkplace()
    {
        state = States.GoingToWorkplace;
    }
}
