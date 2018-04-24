using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoungTree2Script : MonoBehaviour {

    public float growthTime = 2f;
    public float currTime = 0;
    public GameObject Tree;
    ObjectOnGrid oog;

    private void Start()
    {
        oog = this.GetComponent<ObjectOnGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oog.placed)
        {
            if (currTime >= growthTime)
            {
                Debug.Log("YoungTree2 creates Tree");
                GameObject tree = Instantiate(Tree);
                tree.transform.position = gameObject.transform.position;
                tree.transform.rotation = gameObject.transform.rotation;
                ObjectOnGrid oogTree = tree.GetComponent<ObjectOnGrid>();
                oogTree.placed = true;

                for (int i = 0; i < oog.Nodes.GetLength(0); i++)
                {
                    for (int j = 0; j < oog.Nodes.GetLength(1); j++)
                    {
                        Debug.Log("i: " + i + " j: " + j + " | grid x:" + oog.Nodes[i, j].gridX + " grid y: " + oog.Nodes[i, j].gridY);
                    }
                }

                oogTree.SetNodes(oog.Nodes);
                Destroy(gameObject);
            }
            else
            {
                currTime += Time.deltaTime;
            }
        }
    }
}
