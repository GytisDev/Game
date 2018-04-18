﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public bool displayGridGizmos;

    Node[,] grid;
    public Transform playerPosition;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;

    [HideInInspector]
    public float nodeDiameter;

    public int gridSizeX;
    public int gridSizeY;

    private void Awake() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    //retuns number of cells in the grid
    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }

    //returns number of cells per row in the grid
    public int Width
    {
        get
        {
            return gridSizeX;
        }
    }

    //returns number of cells per column in the grid
    public int Height
    {
        get
        {
            return gridSizeY;
        }
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = (Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public bool UpdateGrid(int gridX, int gridY, int takesX, int takesY) {

        int subX = takesX / 2 - (takesX + 1) % 2;
        int subY = takesY / 2 - (takesY + 1) % 2;

        if (gridX - subX + takesX >= gridSizeX || gridX - subX < 0 || gridY - subY + takesY >= gridSizeY || gridY - subY < 0)
            return false;

        for (int x = gridX - subX; x < gridX - subX + takesX; x++) {
            for (int y = gridY - subY; y < gridY - subY + takesY; y++) {
                if (!grid[x, y].walkable)
                    return false;
                if (name == "Road") {
                    grid[x, y].road = true;
                    return true;
                }
            }
        }

        for (int x = gridX - subX; x < gridX -subX + takesX; x++) {
            for (int y = gridY - subY; y < gridY - subY + takesY; y++) {
                grid[x, y].walkable = false;
            }
         }

        return true;
    }

    public Node[,] GetNodes(int gridX, int gridY, int takesX, int takesY)
    {

        Node[,] nodes = new Node[takesX, takesY];
        int subX = takesX / 2 - (takesX + 1) % 2;
        int subY = takesY / 2 - (takesY + 1) % 2;

        int i = 0;
        int j = 0;
        for (int x = gridX - subX; x < gridX - subX + takesX; x++)
        {
            j = 0;
            for (int y = gridY - subY; y < gridY - subY + takesY; y++)
            {
                nodes[i,j] = grid[x, y];
                j++;
            }
            i++;
        }

        return nodes;
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition) {
        float percentX = ((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float percentY = ((worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        if (worldPosition.x < 0)
            x--;
        if (worldPosition.z < 0)
            y--;

        return grid[x, y];
    }

    public Node GetNode(int x, int y)
    {
        return grid[x, y];
    }

    public List<Node> path;
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (grid != null && displayGridGizmos) {
                foreach (Node node in grid) {
                    
                    Gizmos.color = (node.walkable) ? Color.white : Color.red;

                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
                
            }
        }

    //returns node position in the world
    public Vector3 GetNodePosition(int x, int y)
    {
        return grid[x, y].worldPosition;
    }

    //sets cell as occupied
    public void SetOccupied(int x, int y)
    {
        grid[x, y].walkable = false;
    }

	//sets cell as walkable
	public void SetWalkable(int x, int y)
	{
		grid[x, y].walkable = true;
	}

    //returns is cell walkable or not
    public bool IsWalkable(int x, int y)
    {
        return grid[x, y].walkable;
    }

}
