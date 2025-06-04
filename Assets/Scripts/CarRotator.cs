using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotator : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private bool isRotating = false;

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    public void StopRotation()
    {
        isRotating = false;
    }
}
