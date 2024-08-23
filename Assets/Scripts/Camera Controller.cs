using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // Karakterin transformu
    public float smoothSpeed = 0.125f;  // Kameranýn yumuþak hareketi için
    public Vector3 offset;    // Kameranýn karaktere göre konumu

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = transform.position.z;  // Z eksenini sabit tutmak için

        transform.position = smoothedPosition;
    }
}
