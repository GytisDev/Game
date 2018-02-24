using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {

    private Transform currentBuilding;
    public Grid grid;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (currentBuilding != null) {

            ObjectOnGrid data = currentBuilding.GetComponent<ObjectOnGrid>();

            RaycastHit hit = new RaycastHit();
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);

            Vector3 pos = new Vector3(hit.point.x, 0, hit.point.z);

            currentBuilding.transform.position = RoundTransform(pos, grid.nodeDiameter);

            if (Input.GetMouseButtonDown(0)) {
                //Vector3 Position = currentBuilding.position;
                //if(Position.x < 0)
                Node node = grid.NodeFromWorldPoint(currentBuilding.position);
                data.gridPosX = node.gridX;
                data.gridPosY = node.gridY;

                
                if(grid.UpdateGrid(data.gridPosX, data.gridPosY, data.takesSpaceX, data.takesSpaceY))
                    currentBuilding = null;
            }

            if (Input.GetMouseButtonDown(1)) {
                currentBuilding.Rotate(Vector3.forward, 90f);
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
    }
}
