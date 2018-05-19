using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoungTree1Script : MonoBehaviour {

    public float growthTime = 2f;
    public float currTime = 0;
    public GameObject YoungTree2;
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
                Debug.Log("YoungTree1 creates YoungTree2");
                GameObject youngTree2 = Instantiate(YoungTree2);
                youngTree2.transform.position = gameObject.transform.position;
                youngTree2.transform.rotation = gameObject.transform.rotation;
                ObjectOnGrid oog2 = youngTree2.GetComponent<ObjectOnGrid>();
                oog2.placed = true;
                oog2.SetNodes(oog.Nodes);

                //for (int i = 0; i < oog2.Nodes.GetLength(0); i++)
                //{
                //    for (int j = 0; j < oog2.Nodes.GetLength(1); j++)
                //    {
                //        //Debug.Log("i: " + i + " j: " + j + " | grid x:" + oog2.Nodes[i , j].gridX + " grid y: " + oog2.Nodes[i , j].gridY);
                //    }
                //}

                Destroy(gameObject);
            }
            else
            {
                currTime += Time.deltaTime;
            }
        }
    }
}
