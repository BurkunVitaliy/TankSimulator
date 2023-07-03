using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform turret;
    public float mouseSensivity = 50f;
    private float xRotation = 0f;
    private float yRotation = 0f;


    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -20f, 15f);
        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -30f, 30f);
        
        turret.localRotation = Quaternion.Euler(xRotation,0f,yRotation);
    }
}
