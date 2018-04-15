using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoungTree1Script : MonoBehaviour {

    public float growthTime = 2f;
    public float currTime = 0;
    public GameObject YoungTree2;

    // Update is called once per frame
    void Update()
    {
        if (currTime >= growthTime)
        {
            GameObject youngTree2 = Instantiate(YoungTree2);
            youngTree2.transform.position = gameObject.transform.position;
            youngTree2.transform.rotation = gameObject.transform.rotation;
            youngTree2.GetComponent<ObjectOnGrid>().placed = true;
            Destroy(gameObject);
        }
        else
        {
            currTime += Time.deltaTime;
        }
    }
}
