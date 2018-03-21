using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Baker : MonoBehaviour {

    public NavMeshSurface surface;

	// Use this for initialization
	void Start () {
        surface.BuildNavMesh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RebakeSurface() {
        surface.BuildNavMesh();
    }
}
