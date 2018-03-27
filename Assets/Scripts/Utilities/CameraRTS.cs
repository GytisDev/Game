using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRTS : MonoBehaviour
{

    public bool panningWithMouse = true;
    public float panSpeed = 20f;
    public float scrollSpeed = 40f;
    public float smoothingSpeed = 10f;
    public float heightKoef = 0.05f;

    public float panBorderThickness = 10f;

    public float minY = 20f;
    public float maxY = 120f;
    public float minAngle = 40f;
    public float maxAngle = 90f;
    Quaternion originalRotation;
    Vector3 pos;

    private Vector3 dir;
    void Start()
    {
        originalRotation = transform.rotation;
        pos = transform.position;
        RecalculateCameraAngle();
    }

    //reset rotation
    public void ResetRotation()
    {
        transform.rotation = originalRotation;

    }
    // Update is called once per frame
    void Update()
    {
        float _heightKoef = Mathf.Pow(pos.y, 0.5f) * heightKoef;

        if (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - panBorderThickness && panningWithMouse))
        {
            Vector3 angles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(45f, angles.y, angles.z);
            dir = transform.forward;
            dir.y = 0;
            pos += dir * panSpeed * _heightKoef * Time.deltaTime;
            transform.eulerAngles = angles;
        }
        if (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - panBorderThickness && panningWithMouse))
        {
            dir = transform.right;
            dir.y = 0;

            pos += dir * panSpeed * _heightKoef * Time.deltaTime;
        }
        if (Input.GetKey("s") || (Input.mousePosition.y <= panBorderThickness && panningWithMouse))
        {
            Vector3 angles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(45f, angles.y, angles.z);
            dir = -transform.forward;
            dir.y = 0;

            pos += dir * panSpeed * _heightKoef * Time.deltaTime;
            transform.eulerAngles = angles;
        }
        if (Input.GetKey("a") || (Input.mousePosition.x <= panBorderThickness && panningWithMouse))
        {
            dir = -transform.right;
            dir.y = 0;

            pos += dir * panSpeed * _heightKoef * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (pos.y > minY)
                pos += transform.forward * scrollSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (pos.y < maxY)
                pos -= transform.forward * scrollSpeed * Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(Vector3.up, 2f, Space.World);
        }
        if (Input.GetKey("q"))
        {
            transform.Rotate(Vector3.down, 2f, Space.World);
        }

        Grid grid = GameObject.FindObjectOfType<Grid>();
        float mapX = grid.gridSizeX * grid.nodeRadius;
        float mapY = grid.gridSizeY * grid.nodeRadius;

        pos.x = Mathf.Clamp(pos.x, -mapX, mapX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -mapY - 5f, mapY - 5f);

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * smoothingSpeed);
    }

    private void LateUpdate()
    {
        RecalculateCameraAngle();
    }

    void RecalculateCameraAngle()
    {
        float newXRotation = minAngle + ((maxAngle - minAngle) * (transform.position.y - minY) / (maxY - minY));

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(newXRotation, transform.eulerAngles.y, transform.eulerAngles.z), Time.deltaTime * smoothingSpeed);
    }
}
