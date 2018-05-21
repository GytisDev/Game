using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetScript : MonoBehaviour
{

    public Camera Camera;
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
    bool isZero = false;

    Vector3 pos;
    float mapX, mapY, _heightKoef;

    private Vector3 dir;
    void Start()
    {
        pos = transform.position;
        pos.y = 5f;
        Grid grid = GameObject.FindObjectOfType<Grid>();
        mapX = grid.gridSizeX * grid.nodeRadius;
        mapY = grid.gridSizeY * grid.nodeRadius;
    }

    void Update()
    {
        _heightKoef = Mathf.Pow(Camera.transform.position.y, 0.5f) * heightKoef;
        float ts = Time.timeScale;
        if(ts == 0) {
            return;
        } 

        if (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - panBorderThickness && panningWithMouse))
        {
            dir = transform.forward;
            pos += dir * panSpeed * _heightKoef * Time.deltaTime / ts;
        }
        if (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - panBorderThickness && panningWithMouse))
        {
            dir = transform.right;
            pos += dir * panSpeed * _heightKoef * Time.deltaTime / ts;
        }
        if (Input.GetKey("s") || (Input.mousePosition.y <= panBorderThickness && panningWithMouse))
        {
            dir = -transform.forward;
            pos += dir * panSpeed * _heightKoef * Time.deltaTime / ts;
        }
        if (Input.GetKey("a") || (Input.mousePosition.x <= panBorderThickness && panningWithMouse))
        {
            dir = -transform.right;
            pos += dir * panSpeed * _heightKoef * Time.deltaTime / ts;
        }
        if (Input.GetKey("e") || (Input.mousePosition.x <= panBorderThickness && panningWithMouse))
        {
            transform.Rotate(Vector3.up, 150f * Time.deltaTime / ts);
        }
        if (Input.GetKey("q") || (Input.mousePosition.x <= panBorderThickness && panningWithMouse))
        {
            transform.Rotate(Vector3.down, 150f * Time.deltaTime / ts);
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit = new RaycastHit();
        //    Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        //    Physics.Raycast(ray, out hit, Mathf.Infinity);

        //    JumpTo(new Vector3(hit.point.x, 1f, hit.point.z));
        //}

        pos.x = Mathf.Clamp(pos.x, -mapX, mapX);
        pos.z = Mathf.Clamp(pos.z, -mapY - 5f, mapY - 5f);

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * smoothingSpeed);
    }

    public void JumpTo(Vector3 target)
    {
        pos = target;
    }

}
