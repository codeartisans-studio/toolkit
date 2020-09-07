using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;

    public float rotateSpeed = 5;
    public float moveSpeed = 5;
    public float zoomSpeed = 5;

    public bool clampPosition = true;

    public Vector3 positionMin = new Vector3(-10, -5, -10);
    public Vector3 positionMax = new Vector3(10, 5, 0);

    public bool clampRotation = true;
    public Vector2 rotationMin = new Vector2(-90, -180);
    public Vector2 rotationMax = new Vector2(90, 180);

    Vector3 totalPositionDelta = Vector3.zero;
    Vector2 totalRotationDelta = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 positionDelta = Vector3.zero;
        Vector2 rotationDelta = Vector2.zero;
        Vector3 center = Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            // Rotate
            rotationDelta.x = -y * rotateSpeed;
            rotationDelta.y = x * rotateSpeed;

            if (target)
                center = target.position;
        }
        else if (Input.GetMouseButton(1))
        {
            // Move
            positionDelta.x = -x * moveSpeed;
            positionDelta.y = -y * moveSpeed;
        }

        // Zoom
        positionDelta.z = Input.mouseScrollDelta.y;

        if (clampPosition)
        {
            if (totalPositionDelta.x + positionDelta.x > positionMax.x)
                positionDelta.x = positionMax.x - totalPositionDelta.x;
            else if (totalPositionDelta.x + positionDelta.x < positionMin.x)
                positionDelta.x = positionMin.x - totalPositionDelta.x;
            if (totalPositionDelta.y + positionDelta.y > positionMax.y)
                positionDelta.y = positionMax.y - totalPositionDelta.y;
            else if (totalPositionDelta.y + positionDelta.y < positionMin.y)
                positionDelta.y = positionMin.y - totalPositionDelta.y;
            if (totalPositionDelta.z + positionDelta.z > positionMax.z)
                positionDelta.z = positionMax.z - totalPositionDelta.z;
            else if (totalPositionDelta.z + positionDelta.z < positionMin.z)
                positionDelta.z = positionMin.z - totalPositionDelta.z;
        }

        if (clampRotation)
        {
            if (totalRotationDelta.x + rotationDelta.x > rotationMax.x)
                rotationDelta.x = rotationMax.x - totalRotationDelta.x;
            else if (totalRotationDelta.x + rotationDelta.x < rotationMin.x)
                rotationDelta.x = rotationMin.x - totalRotationDelta.x;
            if (totalRotationDelta.y + rotationDelta.y > rotationMax.y)
                rotationDelta.y = rotationMax.y - totalRotationDelta.y;
            else if (totalRotationDelta.y + rotationDelta.y < rotationMin.y)
                rotationDelta.y = rotationMin.y - totalRotationDelta.y;
        }

        transform.RotateAround(center, transform.right, rotationDelta.x);
        transform.RotateAround(center, Vector3.up, rotationDelta.y);

        transform.position += transform.rotation * positionDelta;

        totalPositionDelta += positionDelta;
        totalRotationDelta += rotationDelta;
    }
}
