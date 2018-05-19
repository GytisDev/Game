using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterScript : MonoBehaviour {

    public enum States {GoingToWorkplace, Working, CarryingGoods, PathFinding, Available };
    public States state;
    ResourceManager rm;
    public WoodCutterHutController wcc;
    public GameObject citizen;
    public int Radius = 8;
    public int WoodAccumulation = 5;
    private int WoodGained;
    public float ChopingTime = 5f;
    float currentChopingTime = 0f;
    public float FindingTime = 2f;
    float currentFindingTime = 0f;
    bool hasWorkToDo = false;
    bool noTreesInArea = false;
    GameObject currenTree;
    Unit unit;

    Vector3 lastPos, currentPos;

    // Use this for initialization
    void Start () {
        rm = FindObjectOfType<ResourceManager>();
        lastPos = gameObject.transform.position;
        unit = GetComponent<Unit>();
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
                //if (rm.noTrees) return;
                //if (!hasWorkToDo)
                //{
                //    Work();
                //}
                if (hasWorkToDo) return;
                if (!noTreesInArea)
                    Work();
                else {
                    if (currentFindingTime < FindingTime)
                        currentFindingTime += Time.deltaTime;
                    else {
                        currentFindingTime = 0f;
                        Work();
                    }
                }
                break;

            case States.PathFinding:
                if (currenTree == null)
                    ResetWork();

                if (ArrivedAtTarget(currenTree))
                {
                    state = States.Working;
                    //Debug.Log("Arived at tree");
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
        ObjectOnGrid oog = currenTree.GetComponent<ObjectOnGrid>();
        oog.grid.SetWalkable(oog.gridPosX, oog.gridPosY);

        currenTree = null;
        currentChopingTime = 0f;
        state = States.CarryingGoods;

        unit.MoveTo(wcc.InitialPosition.transform.position);
    }

    public bool ArrivedAtTarget(GameObject targetObject)
    {
        return unit.ArrivedAtTarget(targetObject.transform);
    }

    public void Work()
    {
        if (rm.wood < rm.storage)
        {
            //FindTree();
            FindNearestTree();

            if (currenTree == null)
            {
                //rm.noTrees = true;
                noTreesInArea = true;
                return;
            }

            unit.MoveTo(currenTree.transform.position);
            hasWorkToDo = true;
            state = States.PathFinding;
        }
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

    void FindNearestTree()
    {
        currenTree = null;
        float dist = float.MaxValue;
        Grid grid = FindObjectOfType<Grid>();
        Node HutNode = grid.NodeFromWorldPoint(transform.position);
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree"))
        {
            Node treeNode = grid.NodeFromWorldPoint(tree.transform.position);

            if (treeNode.gridX <= HutNode.gridX + Radius &&
                treeNode.gridX >= HutNode.gridX - Radius &&
                treeNode.gridY <= HutNode.gridY + Radius &&
                treeNode.gridY >= HutNode.gridY - Radius)

            {
                TreeScript ts = tree.GetComponent<TreeScript>();
                ObjectOnGrid oog = tree.GetComponent<ObjectOnGrid>();
                if (ts.available && oog.placed)
                {
                    float currTreeDist = Mathf.Sqrt(Mathf.Pow(treeNode.gridX - HutNode.gridX, 2) + 
                                                  Mathf.Pow(treeNode.gridY - HutNode.gridY, 2));
                    if (currTreeDist < dist)
                    {
                        currenTree = tree;
                        dist = currTreeDist;
                    }
                    //ts.available = false;
                    //return;
                }
            }
        }

        if (currenTree != null)
        {
            TreeScript ts = currenTree.GetComponent<TreeScript>();
            ts.available = false;
        }
    }

    void GoodsArrived()
    {
        // Here you can update resourses using
        // WoodGained - wood gained from last trip
        //Debug.Log("Goods arrived");

        rm.IncreaseResources(ResourceManager.Resources.Wood, WoodGained);

    }

    public void ResetWork()
    {
        hasWorkToDo = false;
        state = States.GoingToWorkplace;
        unit.MoveTo(wcc.InitialPosition.transform.position);
    }

    public void ChangeState_GoingToWorkplace()
    {
        state = States.GoingToWorkplace;
    }
}
