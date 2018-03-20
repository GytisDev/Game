using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    public int woodLeft;
    public bool available;
    public float scale;
    public float growrate;
    // Use this for initialization
	void Start () {
        available = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (available == false)
        {
            Grow();
        }
    }

    public void Grow()
    {
        if (scale < 0.4)
        {
            scale = scale + Time.deltaTime * Random.Range(1,1000) * growrate * 0.0001F;
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else available = true;
    }

    public int Chop(int quantity)
    {
        if (available == true)
        {
            if (quantity > woodLeft)
                quantity = woodLeft;
            woodLeft -= quantity;
        }
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
