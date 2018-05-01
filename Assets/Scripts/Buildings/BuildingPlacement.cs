using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingPlacement : MonoBehaviour {

    public Camera camera;
    GameObject lastBuilding;
    [HideInInspector]
    public Transform currentBuilding;
    public Grid grid;
    private ResourceManager rm;
    bool placed = false;
    Vector3 FortressPos;

    // Use this for initialization
    void Start() {
        rm = FindObjectOfType<ResourceManager>();
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

            currentBuilding.transform.position = rayPointNodePos + new Vector3((oog.takesSpaceX + 1) % 2 * grid.nodeDiameter / 2, 0.5f, (oog.takesSpaceY + 1) % 2 * grid.nodeDiameter / 2);

            if (Input.GetMouseButtonDown(0)) {

                FortressScript Fortress = FindObjectOfType<FortressScript>();
                if (Fortress != null)
                {
                    Debug.Log("qafdasdagasdfgasfg");
                    FortressPos = Fortress.SpawnPosition.position;
                    NavMeshPath path = new NavMeshPath();
                    NavMesh.CalculatePath(FortressPos, currentBuilding.transform.position, NavMesh.AllAreas, path);
                    if (path.status != NavMeshPathStatus.PathComplete)
                        return;
                }

                Node node = grid.NodeFromWorldPoint(currentBuilding.position);
               

                if (grid.UpdateGrid(rayPointNode.gridX, rayPointNode.gridY, data.takesSpaceX, data.takesSpaceY)) {

                    oog.SetNodes(grid.GetNodes(rayPointNode.gridX, rayPointNode.gridY, data.takesSpaceX, data.takesSpaceY));

                    data.gridPosX = node.gridX;
                    data.gridPosY = node.gridY;

                    rm.DecreaseResources(ResourceManager.Resources.Wood, oog.costWood);
                    rm.DecreaseResources(ResourceManager.Resources.Stone, oog.costStone);
                    oog.placed = true;

                    currentBuilding = null;
                }

                if (oog.objectName == "Road") {
                    SetItem(lastBuilding);
                }

            }

            if (Input.GetMouseButtonDown(1)) {
                Destroy(currentBuilding.gameObject);
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
}
