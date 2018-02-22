using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {

    private Transform currentBuilding;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (currentBuilding != null) {

            RaycastHit hit = new RaycastHit();
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);

            hit.point = new Vector3(Mathf.Round(hit.point.x), 0f, Mathf.Round(hit.point.z));
            currentBuilding.transform.position = hit.point;

            //Vector3 m = Input.mousePosition;
            //m = new Vector3(m.x, m.y, transform.position.y);
            //Vector3 p = Camera.main.ScreenToWorldPoint(m);
            //currentBuilding.position = new Vector3(p.x, 0, p.z);

            if (Input.GetMouseButtonDown(0)) {
                currentBuilding = null;
            }

            if (Input.GetMouseButtonDown(1)) {
                currentBuilding.Rotate(Vector3.forward, 90f);
            }

        }
    }

    public void SetItem(GameObject b) {
        currentBuilding = ((GameObject)Instantiate(b)).transform;
    }
}
