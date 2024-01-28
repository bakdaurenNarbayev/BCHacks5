using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public Transform centerOfRotation;  // Reference to the center of rotation
    public float rotationSpeed = 5f;   // Rotation speed in degrees per second

    void Update()
    {
        // Ensure that there is a reference to the center of rotation
        if (centerOfRotation == null)
        {
            Debug.LogError("Center of rotation is not set!");
            return;
        }

        // Calculate the rotation axis
        Vector3 rotationAxis = centerOfRotation.up;

        // Rotate the object around the center of rotation
        transform.RotateAround(centerOfRotation.position, rotationAxis, rotationSpeed * Time.deltaTime);
    }
}