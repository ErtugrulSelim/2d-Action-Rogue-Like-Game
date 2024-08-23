using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // Karakterin transformu
    public float smoothSpeed = 0.125f;  // Kameran�n yumu�ak hareketi i�in
    public Vector3 offset;    // Kameran�n karaktere g�re konumu

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = transform.position.z;  // Z eksenini sabit tutmak i�in

        transform.position = smoothedPosition;
    }
}
