﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterScript : MonoBehaviour {

    public enum States {GoingToWorkplace, Working, CarryingGoods, PathFinding, Available };
    public States state;
    public WoodCutterHutController wcc;
    public GameObject citizen;
    public int Radius = 30;
    public float ChopingTime = 3f;
    float currentChopingTime = 0;
    bool hasWorkToDo = false;
    GameObject currenTree;

    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (citizen == null) return;
        switch (state)
        {
            case States.GoingToWorkplace:
                Debug.Log(ArrivedAtTarget(wcc.InitialPosition).ToString());
                if (ArrivedAtTarget(wcc.InitialPosition))
                {
                    state = States.Available;
                }
                break;

            case States.Available:
                if (!hasWorkToDo)
                {
                    Work();
                } else
                {

                }
                break;

            case States.PathFinding:
                if (ArrivedAtTarget(currenTree))
                {
                    state = States.Working;
                }
                break;

            case States.Working:
                if(currentChopingTime < ChopingTime)
                {
                    currentChopingTime += Time.deltaTime;
                } else
                {
                    TreeChoped();
                }
                break;

            case States.CarryingGoods:
                if (ArrivedAtTarget(wcc.InitialPosition))
                {
                    state = States.Available;
                    hasWorkToDo = false;

                }
                break;

            default:

                break;
        }
	}

    public void TreeChoped()
    {
        currenTree = null;
        currentChopingTime = 0f;
        state = States.CarryingGoods;

        Unit unit = citizen.GetComponent<Unit>();
        unit.target = wcc.InitialPosition.transform;
    }

    public bool ArrivedAtTarget(GameObject targetObject)
    {
        return citizen.GetComponent<Unit>().ArivedAtTarget(targetObject);
    }

    public void Work()
    {
        FindTree();

        if (currenTree == null) return;

        Unit unit = citizen.GetComponent<Unit>();
        unit.target = currenTree.transform;
        hasWorkToDo = true;
        state = States.PathFinding;
    }

    void FindTree()
    {
        Grid grid = FindObjectOfType<Grid>();
        Node HutNode = grid.NodeFromWorldPoint(transform.position);
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree"))
        {
            Node treeNode = grid.NodeFromWorldPoint(tree.transform.position);

            if (treeNode.gridX <= HutNode.gridX + Radius &&
                treeNode.gridX >= HutNode.gridX - Radius &&
                treeNode.gridY <= HutNode.gridY + Radius &&
                treeNode.gridY >= HutNode.gridY - Radius)

            {
                currenTree = tree;
                return;
            }
        }
    }

    public void ChangeState_GoingToWorkplace()
    {
        state = States.GoingToWorkplace;
    }
}
