using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasScript : MonoBehaviour {

    public GameObject MainCamera;
    // Use this for initialization
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = MainCamera.transform.rotation;
        transform.rotation = rot;
    }
}
