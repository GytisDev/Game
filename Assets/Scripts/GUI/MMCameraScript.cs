using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCameraScript : MonoBehaviour {

    public float scrollSpeed = 40f;
    public Camera m_OrthographicCamera;
    public float minY = 5f;
    public float maxY = 20f;

    // Update is called once per frame
    private void Update()
    {
        Vector3 pos = Camera.main.transform.position;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //zoom in
        {
            if (pos.y > minY)
            {
                m_OrthographicCamera.orthographicSize -= scrollSpeed * Time.deltaTime;
                pos -= transform.forward * scrollSpeed * Time.deltaTime;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //zoom out
        {
            if (pos.y < maxY)
            {
                m_OrthographicCamera.orthographicSize += scrollSpeed * Time.deltaTime;
                pos += transform.forward * scrollSpeed * Time.deltaTime;
            }

        }
    }
}
