using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    private NavMeshAgent agent;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (agent.remainingDistance == Mathf.Infinity) {
            agent.destination = transform.position;
        }
    }

    public void MoveTo(Vector3 position) {
        agent.destination = position;
    }

    public bool ArrivedAtTarget() {

        if (!agent.pathPending) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                    return true;
                }
            }
        }

        return false;
    }

    public bool ArrivedAtTarget(Transform transformTarget) {
        if (Vector3.Distance(transform.position, transformTarget.position) <= agent.stoppingDistance) {
            return true;
        }

        return false;
    }
}
