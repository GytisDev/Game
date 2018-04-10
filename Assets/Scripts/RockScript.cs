using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour {

    public int stoneLeft;
    public bool available;

    // Use this for initialization
    void Start()
    {
        available = true;
    }

    public int Mine(int quantity)
    {
        //if (available == true)
        //{
        //    if (quantity > stoneLeft)
        //        quantity = stoneLeft;
        //    stoneLeft -= quantity;
        //}

        if (quantity > stoneLeft)
            quantity = stoneLeft;
        stoneLeft -= quantity;

        available = true;

        return quantity;
    }

    public void CutDownIfEmpty()
    {
        if (stoneLeft <= 0)
        {
            Grid grid = GameObject.FindObjectOfType<Grid>();
            Node treeNode = grid.NodeFromWorldPoint(transform.position);
            treeNode.walkable = true;
            Destroy(gameObject);
        }
    }
}
