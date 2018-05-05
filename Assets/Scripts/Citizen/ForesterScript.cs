using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForesterScript : MonoBehaviour {

    public enum States { GoingToWorkplace, Working, CommingBack, PathFinding, Available };
    public States state;
    public ForestersHutScript fhs;
    public GameObject citizen;
    public int Radius;
    public float PlantingTime = 4f;
    public float FindingTime = 2f;

    ResourceManager rm;
    float currentPlantingTime = 0f;
    float currentFindingTime = 0f;
    bool hasWorkToDo = false;
    bool noSpaceInArea = false;
    GameObject newTreeLocation;
    Node targetNode = null;
    Unit unit;

    Vector3 lastPos;

    // Use this for initialization
    void Start()
    {
        rm = FindObjectOfType<ResourceManager>();
        lastPos = gameObject.transform.position;
        unit = GetComponent<Unit>();
        Radius = fhs.Radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (citizen == null) return;

        switch (state)
        {
            case States.GoingToWorkplace:
                if (ArrivedAtTarget(fhs.InitialPosition))
                {
                    state = States.Available;
                }
                break;

            case States.Available:
                if (hasWorkToDo) return;
                if (!noSpaceInArea)
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

                if (ArrivedAtTarget(newTreeLocation))
                {
                    state = States.Working;
                    Debug.Log("Arived at tree planting location");
                }
                break;

            case States.Working:
                if (newTreeLocation == null)
                    ResetWork();

                if (currentPlantingTime < PlantingTime)
                {
                    currentPlantingTime += Time.deltaTime;
                }
                else
                {
                    PlantTree();
                    currentPlantingTime = 0f;
                }
                break;

            case States.CommingBack:
                if (ArrivedAtTarget(fhs.InitialPosition))
                {
                    state = States.Available;
                    hasWorkToDo = false;
                }
                break;

            default:

                break;
        }
    }

    public void PlantTree()
    {
        GameObject youngTree = Instantiate(fhs.YoungTree);
        youngTree.transform.position = newTreeLocation.transform.position;
        ObjectOnGrid oogNewTree = youngTree.GetComponent<ObjectOnGrid>();
        oogNewTree.placed = true;
        oogNewTree.SetNodes(new Node[,] { { targetNode } });

        newTreeLocation = null;
        targetNode = null;
        state = States.CommingBack;

        unit.MoveTo(fhs.InitialPosition.transform.position);
    }

    public bool ArrivedAtTarget(GameObject targetObject)
    {
        return unit.ArrivedAtTarget(targetObject.transform);
    }

    public void Work()
    {
        noSpaceInArea = false;
        FindPlaceForTree2();
        if (noSpaceInArea)
            return;

        unit.MoveTo(newTreeLocation.transform.position);
        hasWorkToDo = true;
        state = States.PathFinding;
    }

    
    void FindPlaceForTree2()
    {
        newTreeLocation = null;
        targetNode = null;
        Grid grid = FindObjectOfType<Grid>();
        Node HutNode = grid.NodeFromWorldPoint(transform.position);
        Node currentTargetNode;
        int x = -1;
        int y = -1;

        List<Node> area = new List<Node>();
        for (int i = -Radius; i < Radius; i++)
        {
            for (int j = -Radius; j < Radius; j++)
            {
                x = HutNode.gridX + i;
                y = HutNode.gridY + j;
                currentTargetNode = grid.GetNode(x, y);
                if (currentTargetNode.walkable)
                    if(Vector3.Distance(currentTargetNode.worldPosition, fhs.transform.position) <= Radius*grid.nodeRadius*2)
                        area.Add(currentTargetNode);
            }
        }

        if (area.Count > 0)
        {
            targetNode = area[Random.Range(0, area.Count - 1)];
            targetNode.walkable = false;
            area.Remove(targetNode);
            noSpaceInArea = false;
            newTreeLocation = new GameObject();
            newTreeLocation.transform.position = targetNode.worldPosition;
        }
        else
            noSpaceInArea = true;

    }

    public void ResetWork()
    {
        hasWorkToDo = false;
        state = States.GoingToWorkplace;
        unit.MoveTo(fhs.InitialPosition.transform.position);
    }

    public void ChangeState_GoingToWorkplace()
    {
        state = States.GoingToWorkplace;
    }
}
