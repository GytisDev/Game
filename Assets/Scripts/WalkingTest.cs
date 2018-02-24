using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit = new RaycastHit();
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(cameraRay, out hit, Mathf.Infinity);

        hit.point = new Vector3(Mathf.Round(hit.point.x), 0f, Mathf.Round(hit.point.z));

        if (Input.GetMouseButton(1)) {
            transform.position = hit.point;
        }
    }
}
