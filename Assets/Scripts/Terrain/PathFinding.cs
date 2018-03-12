﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    PathRequestManager requestManager;
    Grid grid;

    private void Awake() {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
        print("Coroutine started to find path");
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {

        Vector3[] waypoints = new Vector3[0];
        bool pathSucces = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if(startNode.walkable && targetNode.walkable) { 
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

                while (openSet.Count > 0) {
                    Node currentNode = openSet.RemoveFirst();
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode) {
                        pathSucces = true;

                        break;
                    }

                    foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
                        if (!neighbour.walkable || closedSet.Contains(neighbour))
                            continue;

                        int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, targetNode);
                            neighbour.parent = currentNode;

                            if (!openSet.Contains(neighbour)) {
                                openSet.Add(neighbour);
                            }
                            else {
                                openSet.UpdateItem(neighbour);
                            }
                        }
                    }
                }
        }

        yield return null;

        if (pathSucces) {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSucces);
    }

    Vector3[] RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();

        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        Vector3[] waypoints = simplifyPath(path);
        Array.Reverse(waypoints);

        print(path.Count);

        return waypoints;
    }

    Vector3[] simplifyPath(List<Node> path) {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++) {
            Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld) {
                waypoints.Add(path[i].worldPosition);
            }

            directionOld = directionNew;
        }

        return waypoints.ToArray();

    }

    int GetDistance(Node a, Node b) {
        int disX = Mathf.Abs(a.gridX - b.gridX);
        int disY = Mathf.Abs(a.gridY - b.gridY);

        if(disX > disY) {
            return 14 * disY + 10 * (disX - disY);
        }
        return 14 * disX + 10 * (disY - disX);
    }
}
