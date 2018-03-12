using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Transform target = null;
    Vector3 oldTarget;
    public float speed;
    bool pathWasNotFoundSingleTime = false;

    Vector3[] path;

    int targetIndex;

    private void Start() {
        if (target == null)
            target = transform;

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

    public bool ArivedAtTarget(GameObject targetObject)
    {
        if (!targetObject)
            return false;

        bool arived = false;
        Grid grid = FindObjectOfType<Grid>();
        Node targetNode = grid.NodeFromWorldPoint(targetObject.transform.position);
        Node currentNode = grid.NodeFromWorldPoint(transform.position);

        if (currentNode.gridX <= targetNode.gridX + 2 && // + 1
            currentNode.gridX >= targetNode.gridX - 2 && // - 1
            currentNode.gridY <= targetNode.gridY + 2 && // + 1
            currentNode.gridY >= targetNode.gridY - 2 ) // - 1
            arived = true;

        return arived;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccesful) {
        if (pathSuccesful) {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else {
            pathWasNotFoundSingleTime = true;
        }
        if(pathWasNotFoundSingleTime){
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
    }

    IEnumerator FollowPath() {
        targetIndex = 0;

        if (path.Length > 0)
        {
            Vector3 currentWaypoint = path[0];
            float speedvalue;
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
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

        pathWasNotFoundSingleTime = false;
    }
}
