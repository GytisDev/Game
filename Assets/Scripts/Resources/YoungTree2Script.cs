using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoungTree2Script : MonoBehaviour {

    public float growthTime = 2f;
    public float currTime = 0;
    public GameObject Tree;

    // Update is called once per frame
    void Update()
    {
        if (currTime >= growthTime)
        {
            GameObject tree = Instantiate(Tree);
            tree.transform.position = gameObject.transform.position;
            tree.transform.rotation = gameObject.transform.rotation;
            tree.GetComponent<ObjectOnGrid>().placed = true;
            Destroy(gameObject);
        }
        else
        {
            currTime += Time.deltaTime;
        }
    }
}
