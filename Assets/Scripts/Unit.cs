using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Transform target;
    Vector3 oldTarget;
    public float speed;


    Vector3[] path;

    int targetIndex;

    private void Start() {
        oldTarget = target.position;
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    private void Update() {
        
        if(target.position != oldTarget) {
            targetIndex = 0;
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            oldTarget = target.position;
            
        }

    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccesful) {
        if (pathSuccesful) {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];
        float speedvalue;
        while (true) {
            if(transform.position == currentWaypoint) {
                targetIndex++;
                if(targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            if (PauseMenu.GameIsPaused)
                speedvalue = 0;
            else
                speedvalue = speed;
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speedvalue);
            yield return null;
        }
    }
}
