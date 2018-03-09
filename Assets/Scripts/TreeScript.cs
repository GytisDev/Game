using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    public int woodLeft;
    public bool available;
    // Use this for initialization
	void Start () {
        available = true;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public int Chop(int quantity)
    {
        available = true;
        if (quantity > woodLeft)
            quantity = woodLeft;
        woodLeft -= quantity;

        return quantity;
    }

    public void ChopDownIfEmpty()
    {
        if(woodLeft <= 0)
        {
            Grid grid = GameObject.FindObjectOfType<Grid>();
            Node treeNode = grid.NodeFromWorldPoint(transform.position);
            treeNode.walkable = true;
            Destroy(gameObject);
        }
    }
}
