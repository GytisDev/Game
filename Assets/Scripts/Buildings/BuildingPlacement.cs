using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {

    public Camera camera;
    private Transform currentBuilding;
    public Grid grid;
    private ResourceManager rm;
    bool placed = false;

    // Use this for initialization
    void Start() {
        rm = FindObjectOfType<ResourceManager>();
    }

    // Update is called once per frame
    void Update() {
        

        if (currentBuilding != null && !placed) {

            ObjectOnGrid data = currentBuilding.GetComponent<ObjectOnGrid>();

            RaycastHit hit = new RaycastHit();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);

            Vector3 pos = new Vector3(hit.point.x, 0, hit.point.z);

            Node rayPointNode = grid.NodeFromWorldPoint(hit.point);
            Vector3 rayPointNodePos = rayPointNode.worldPosition;
            ObjectOnGrid oog = currentBuilding.GetComponent<ObjectOnGrid>();

            print(rayPointNode.worldPosition.x + " " + rayPointNode.worldPosition.z + "\n" + hit.point.x + " " + hit.point.z);

            currentBuilding.transform.position = rayPointNodePos + new Vector3((oog.takesSpaceX + 1) % 2 * grid.nodeDiameter / 2, 0, (oog.takesSpaceY + 1) % 2 * grid.nodeDiameter / 2);

            if (Input.GetMouseButtonDown(0)) {

                Node node = grid.NodeFromWorldPoint(currentBuilding.position);
                

                if (grid.UpdateGrid(rayPointNode.gridX, rayPointNode.gridY, data.takesSpaceX, data.takesSpaceY)) {

                    data.gridPosX = node.gridX;
                    data.gridPosY = node.gridY;

                    rm.DecreaseResources(ResourceManager.Resources.Wood, oog.costWood);
                    rm.DecreaseResources(ResourceManager.Resources.Stone, oog.costStone);
                    oog.placed = true;

                    placed = true;
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
        currentBuilding = ((GameObject)Instantiate(b)).transform;
        placed = false;
    }
}
