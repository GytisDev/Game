using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnGrid : MonoBehaviour{

    [HideInInspector]
    public Grid grid;

    [HideInInspector]
    public int gridPosX, gridPosY;

    public string objectName;
    public int takesSpaceX, takesSpaceY;
    public int costWood, costStone;
    public int scoreBonus;
    public Node[,] Nodes;

    public bool placed = false;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        //Nodes = new Node[takesSpaceX, takesSpaceY];
    }

    public void MakeWalkable()
    {
        if (Nodes == null || Nodes[0,0] == null)
        {
            Debug.Log("Nodes taken by the objects is not set");
            return;
        }

        for (int i = 0; i < takesSpaceX; i++)
        {
            for (int j = 0; j < takesSpaceY; j++)
            {
                Nodes[i, j].walkable = true;
            }
        }
    }

    public void MakeUnwalkable()
    {
        if (Nodes == null || Nodes[0, 0] == null)
        {
            Debug.Log("Nodes taken by the objects are not set");
            return;
        }

        for (int i = 0; i < takesSpaceX; i++)
        {
            for (int j = 0; j < takesSpaceY; j++)
            {
                Nodes[i, j].walkable = false;
            }
        }
    }

    public void SetNodes(Node[,] Nodes)
    {
        if (Nodes == null || Nodes[0, 0] == null)
        {
            Debug.Log("Nodes taken by the objects are not set");
            return;
        }

        if (takesSpaceX != Nodes.GetLength(0) || takesSpaceY != Nodes.GetLength(1))
            return;

        this.Nodes = new Node[Nodes.GetLength(0), Nodes.GetLength(1)];

        for (int i = 0; i < takesSpaceX; i++)
        {
            for (int j = 0; j < takesSpaceY; j++)
            {
                try
                {
                    this.Nodes[i, j] = Nodes[i, j];
                    Debug.Log("Nodes[i,j] x = " + this.Nodes[i, j].gridX + "Nodes[i,j] y = " + this.Nodes[i, j].gridY);
                }
                catch (System.Exception)
                {
                    Debug.Log("Error.  i = " + i + "    j = " + j);
                    //Debug.Log("this.Nodes[i,j] x = " + this.Nodes[i, j].gridX + "this.Nodes[i,j] y = " + this.Nodes[i, j].gridY);
                    Debug.Log("Nodes[i,j] x = " + Nodes[i, j].gridX + "Nodes[i,j] y = " + Nodes[i, j].gridY);
                }
            }
        }
    }
}
