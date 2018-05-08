using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    public GameObject Light;
    public GameObject Png;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, 7.5f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        if (gameObject.transform.position.y < 0)
        {
            Light.SetActive(false);
            Png.SetActive(false);
        }
        else
        {
            Light.SetActive(true);
            Png.SetActive(true);
        }
    }
}
