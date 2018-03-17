using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassScript : MonoBehaviour {

    public GameObject maincam;

    void Update()
    {
        transform.rotation = Quaternion.Euler (0f, 0f, maincam.transform.eulerAngles.y);
    }
}
