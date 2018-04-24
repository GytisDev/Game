using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    private NavMeshAgent agent;
    private Grid grid;
    private double speedOnRoad;
    private double speedOnDirt;
    Animation anim;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        grid = FindObjectOfType<Grid>();
        speedOnDirt = agent.speed;
        speedOnRoad = speedOnDirt + speedOnDirt;
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update() {
        //if (agent.remainingDistance == Mathf.Infinity) {
        //    agent.destination = transform.position;
        //}
        if(agent.velocity != Vector3.zero) {
            StartAnimation();
        }
        else {
            StopAnimation();
        }

        if (agent.path.status == NavMeshPathStatus.PathPartial)
        {
            agent.destination = transform.position;
        }
        if (IsOnRoad()) {
            agent.speed = (float)speedOnRoad;
        }
        else {
            agent.speed = (float)speedOnDirt;
        }
    }

    private bool IsOnRoad() {
        return grid.NodeFromWorldPoint(transform.position).road;
    }

    public void StartAnimation() {
        anim.Play();
    }

    public void StopAnimation() {
        anim.Stop();
    }

    public void MoveTo(Vector3 position) {
        agent.destination = position;
    }

    public bool ArrivedAtTarget() {

        return (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance + 0.5f);
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
        } 

        return false;
    }
}
