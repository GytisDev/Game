using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Baker : MonoBehaviour {

    public NavMeshSurface surface;
    public bool needsRebaking = false;
    public float timer = 0;

	// Use this for initialization
	void Start () {
        surface.BuildNavMesh();
	}
	
	// Update is called once per frame
	void Update () {
        if (needsRebaking && timer > 1) {
            RebakeSurface();
            needsRebaking = false;
        }

        timer += Time.deltaTime;

        if(timer > 3) {
            timer = 0;
        }
	}

    public void RebakeSurface() {
        surface.BuildNavMesh();
    }
}
