using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRTS : MonoBehaviour {

    public float speed = 20f;
    private Vector3 dir;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKey("w")) {
            dir = transform.forward;
            dir.y = 0;

            transform.position += dir * speed * Time.deltaTime;
        }
        if (Input.GetKey("d")) {
            dir = transform.right;
            dir.y = 0;

            transform.position += dir * speed * Time.deltaTime;
        }
        if (Input.GetKey("s")) {
            dir = -transform.forward;
            dir.y = 0;

            transform.position += dir * speed * Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            dir = -transform.right;
            dir.y = 0;

            transform.position += dir * speed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey("e")) {
            transform.Rotate(Vector3.up, 1f, Space.World);
        }
        if (Input.GetKey("q")) {
            transform.Rotate(Vector3.down, 1f, Space.World);
        }

        //transform.position = pos;
    }
}
