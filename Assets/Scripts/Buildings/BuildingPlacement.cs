using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingPlacement : MonoBehaviour {

    public Camera camera;
    GameObject lastBuilding;
    public GameObject Build0, Build1;
    [HideInInspector]
    public Transform currentBuilding;
    public Grid grid;
    private ResourceManager rm;
    bool placed = false;
    Vector3 FortressPos;
    static GameObject[] Build0s, Build1s;
    static bool indicatorsCreated = false;

    // Use this for initialization
    void Start() {
        rm = FindObjectOfType<ResourceManager>();
        //variableForPrefab = (GameObject)Resources.Load("prefabs/prefab1", typeof(GameObject));
        //Build0 = (GameObject)Resources.Load("Assets/Prefabs/Build0", typeof(GameObject));
        if (!indicatorsCreated)
        {
            Build0s = new GameObject[100];
            Build1s = new GameObject[100];
            for (int i = 0; i < 100; i++)
            {
                Build0s[i] = Instantiate(Build0, new Vector3(0f, -1f, 0f), Quaternion.identity);
                Build1s[i] = Instantiate(Build1, new Vector3(0f, -1f, 0f), Quaternion.identity);
            }
            indicatorsCreated = true;
        }
    }

    // Update is called once per frame
    void Update() {
        

        if (currentBuilding != null) {

            ObjectOnGrid data = currentBuilding.GetComponent<ObjectOnGrid>();
            
            RaycastHit hit = new RaycastHit();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);

            Vector3 pos = new Vector3(hit.point.x, 0, hit.point.z);

            Node rayPointNode = grid.NodeFromWorldPoint(hit.point);
            Vector3 rayPointNodePos = rayPointNode.worldPosition;
            ObjectOnGrid oog = currentBuilding.GetComponent<ObjectOnGrid>();
            NavMeshObstacle navMeshObstacle = currentBuilding.GetComponent<NavMeshObstacle>();

            currentBuilding.transform.position = rayPointNodePos + new Vector3((oog.takesSpaceX + 1) % 2 * grid.nodeDiameter / 2, 0.5f, (oog.takesSpaceY + 1) % 2 * grid.nodeDiameter / 2);

            FortressScript Fortress = FindObjectOfType<FortressScript>();

            if (Fortress == null)
                ShowAvailableToBuild(currentBuilding.transform.position);
            else
                ShowAvailableToBuild(currentBuilding.transform.position, Fortress.transform.position, Fortress.Ranges[Fortress.Level - 1], oog.takesSpaceX, oog.takesSpaceY);

            if (Input.GetMouseButtonDown(0)) {

                Node node = grid.NodeFromWorldPoint(currentBuilding.position);

                if (Fortress != null)
                {
                    //if (Vector3.Distance(Fortress.transform.position, currentBuilding.transform.position) > Fortress.Ranges[Fortress.Level - 1])
                    //    return;

                    if (!grid.CheckDistance(rayPointNode.gridX, rayPointNode.gridY, data.takesSpaceX, data.takesSpaceY, Fortress.transform.position, Fortress.Ranges[Fortress.Level - 1]))
                        return;

                    Debug.Log("qafdasdagasdfgasfg");
                    FortressPos = Fortress.SpawnPosition.position;
                    NavMeshPath path = new NavMeshPath();
                    NavMesh.CalculatePath(FortressPos, currentBuilding.transform.position, NavMesh.AllAreas, path);
                    if (path.status != NavMeshPathStatus.PathComplete)
                        return;
                }

                if (grid.UpdateGrid(rayPointNode.gridX, rayPointNode.gridY, data.takesSpaceX, data.takesSpaceY)) {

                    oog.SetNodes(grid.GetNodes(rayPointNode.gridX, rayPointNode.gridY, data.takesSpaceX, data.takesSpaceY));

                    data.gridPosX = node.gridX;
                    data.gridPosY = node.gridY;

                    rm.DecreaseResources(ResourceManager.Resources.Wood, oog.costWood);
                    rm.DecreaseResources(ResourceManager.Resources.Stone, oog.costStone);
                    oog.placed = true;
                    navMeshObstacle.enabled = true;

                    currentBuilding = null;
                    resetRest(0, 0);
                }

                if (oog.objectName == "Road") {
                    SetItem(lastBuilding);
                }

            }

            if (Input.GetMouseButtonDown(1)) {
                Destroy(currentBuilding.gameObject);
                resetRest(0, 0);
            }

            if (Input.GetKeyDown("r")) {
                currentBuilding.Rotate(Vector3.forward, 90);
            }

        }
    }

    private Vector3 RoundTransform(Vector3 v, float snapValue) {
        return new Vector3
        (
        snapValue * Mathf.Round(v.x / snapValue),
        v.y,
        snapValue * Mathf.Round(v.z / snapValue)
        );
    }

    public void SetItem(GameObject b) {
        lastBuilding = b;
        currentBuilding = Instantiate(b).transform;
        placed = false;
    }

    public void ShowAvailableToBuild(Vector3 currentPos)
    {
        Node targetNode = grid.NodeFromWorldPoint(currentPos);
        int x = targetNode.gridX;
        int y = targetNode.gridY;
        int index0 = 0, index1 = 0;
        Node currentNode;
        Vector3 offset = new Vector3(0f, 1f, 0f);
        Vector3 pos;

        for (int i = x - 5; i < x + 4; i++)
        {
            for (int j = y - 5; j < y + 4; j++)
            {
                if (i < grid.gridSizeX && j < grid.gridSizeY && i > 0 && j > 0)
                {
                    currentNode = grid.GetNode(i,j);
                    pos = currentNode.worldPosition;

                    if (currentNode.walkable)
                    {
                        Build1s[index1].transform.position = pos + offset;
                        index1++;
                    } else
                    {
                        Build0s[index0].transform.position = pos + offset;
                        index0++;
                    }
                }
            }
        }

        resetRest(index0, index1);
    }

    public void ShowAvailableToBuild(Vector3 currentPos, Vector3 fortressPos, float dist, int takesX, int takesY)
    {
        Node targetNode = grid.NodeFromWorldPoint(currentPos);
        int x = targetNode.gridX;
        int y = targetNode.gridY;
        int index0 = 0, index1 = 0;
        Node currentNode;
        Vector3 offset = new Vector3(0f, 1f, 0f);
        Vector3 pos;

        for (int i = x - 4; i < x + 5 + ((takesX + 1) % 2 * 1); i++)
        {
            for (int j = y - 4 ; j < y + 5 + ((takesY + 1) % 2 * 1); j++)
            {
                if (i < grid.gridSizeX && j < grid.gridSizeY && i > 0 && j > 0)
                {
                    currentNode = grid.GetNode(i, j);
                    pos = currentNode.worldPosition;

                    if (currentNode.walkable)
                    {
                        if (Vector3.Distance(fortressPos, pos) <= dist)
                        {
                            Build1s[index1].transform.position = pos + offset;
                            index1++;
                        } else
                        {
                            Build0s[index0].transform.position = pos + offset;
                            index0++;
                        }
                    }
                    else
                    {
                        Build0s[index0].transform.position = pos + offset;
                        index0++;
                    }
                }
            }
        }

        resetRest(index0, index1);
    }

    void resetRest(int index0, int index1)
    {
        Vector3 initialPos = new Vector3(0f, -1f, 0f);

        for (int i = index0; i < Build0s.Length; i++)
        {
            Build0s[i].transform.position = initialPos;
        }

        for (int i = index1; i < Build1s.Length; i++)
        {
            Build1s[i].transform.position = initialPos;
        }
    }
}
