using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRTS : MonoBehaviour {

    public float panSpeed = 20f;
    public float scrollSpeed = 40f;

    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    public float minY = 20f;
    public float maxY = 120f;

    private Vector3 dir;

    // Update is called once per frame
    void Update() {

        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
            dir = transform.forward;
            dir.y = 0;

            pos += dir * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) {
            dir = transform.right;
            dir.y = 0;

            pos += dir * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
            dir = -transform.forward;
            dir.y = 0;

            pos += dir * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) {
            dir = -transform.right;
            dir.y = 0;

            pos += dir * panSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if(pos.y > minY)
                pos += transform.forward * scrollSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            if(pos.y < maxY)
                pos -= transform.forward * scrollSpeed * Time.deltaTime;
        }
        if (Input.GetKey("e")) {
            transform.Rotate(Vector3.up, 2f, Space.World);
        }
        if (Input.GetKey("q")) {
            transform.Rotate(Vector3.down, 2f, Space.World);
        }

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
