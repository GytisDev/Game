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
        //if (agent.remainingDistance == Mathf.Infinity) {
        //    agent.destination = transform.position;
        //}
        if (agent.path.status == NavMeshPathStatus.PathPartial)
        {
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
        //if (Vector3.Distance(transform.position, transformTarget.position) <= agent.stoppingDistance) {
        //    return true;
        //}

        //if (agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.velocity.sqrMagnitude == 0f)
        //{
        //    return true;
        //}

        if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance + 0.5f)
        {
            return true;
        } else
        {
            print("Remaining distance: " + agent.remainingDistance + 
                "\nPath status: " + agent.path.status + 
                "\nVelocity: " + agent.velocity.sqrMagnitude);
        }

        return false;
    }
}
