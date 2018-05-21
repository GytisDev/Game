using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRTS : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    public float minY = 20f;
    public float maxY = 120f;
    public float scrollSpeed = 40f;
    public float smoothingSpeed = 10f;

    Vector3 pos;

    private void Update()
    {
        

        float ts = Time.timeScale;

        if (ts == 0)
            return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            pos = transform.position;
            if (pos.y > minY)
            {
                offsetPosition.y -= scrollSpeed * Time.deltaTime / ts;
                offsetPosition.z += scrollSpeed * 0.5f * Time.deltaTime / ts;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            pos = transform.position;
            if (pos.y < maxY)
            {
                offsetPosition.y += scrollSpeed * Time.deltaTime / ts;
                offsetPosition.z -= scrollSpeed * 0.5f * Time.deltaTime / ts;
            }
                pos -= transform.forward * scrollSpeed * Time.deltaTime / ts;
        }

        pos.y = Mathf.Clamp(pos.y, minY, maxY);
    }

    // LateUpdate is called after Update each frame

    void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            //offsetPosition.y = Mathf.Clamp(offsetPosition.y, minY, maxY);
            transform.position = Vector3.Lerp(transform.position, target.TransformPoint(offsetPosition), 0.5f);
            if (transform.position.y < minY || transform.position.y > maxY) {

                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
            }
        }
        else
        {
            transform.position = target.position + offsetPosition;

        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
