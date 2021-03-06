﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public bool road;
    public Node parent;
    public bool selected;

    public int gCost;
    public int hCost;

    int heapIndex;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        selected = false;
    }

    public int HeapIndex {
        get {
            return heapIndex;
        }

        set {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other) {
        int compare = fCost.CompareTo(other.fCost);
        if(compare == 0) {
            compare = hCost.CompareTo(other.hCost);
        }

        return -compare;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }

}
