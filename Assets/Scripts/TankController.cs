using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float maxSpeed = 40f, acceleration = 20f, brakingForce = 10f, rotationSpeed = 20f;
    private float tiltAngleMin = 0f, tiltAngleMax = 10f;
    private Rigidbody _rigidbody;
    private Transform _tank, _tankBody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _tankBody = transform.GetChild(0).transform;
        _rigidbody.centerOfMass = Vector3.down;
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Horizontal");
        float currentSpeed = _rigidbody.velocity.magnitude;
        float decelerationInput = Input.GetAxis("Jump");
        
        if (currentSpeed < maxSpeed && moveInput > 0)
        {
            Vector3 forwardForce = transform.forward * acceleration;
            _rigidbody.AddForce(forwardForce, ForceMode.Acceleration);
            Quaternion targetRotation = Quaternion.Euler(tiltAngleMax, _tankBody.rotation.eulerAngles.y, _tankBody.rotation.eulerAngles.z);
            _tankBody.rotation = Quaternion.Lerp(_tankBody.rotation, targetRotation, currentSpeed/maxSpeed);
            
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(tiltAngleMin, _tankBody.rotation.eulerAngles.y, _tankBody.rotation.eulerAngles.z);
            _tankBody.rotation = Quaternion.Lerp(_tankBody.rotation, targetRotation, currentSpeed/maxSpeed);
        }

        if (currentSpeed > 0f && moveInput < 0 )
        {
            Vector3 forwardForce = -transform.forward * acceleration;
            _rigidbody.AddForce(forwardForce,  ForceMode.Acceleration);
            Quaternion targetRotation = Quaternion.Euler(-tiltAngleMax, _tankBody.rotation.eulerAngles.y, _tankBody.rotation.eulerAngles.z);
            _tankBody.rotation = Quaternion.Lerp(_tankBody.rotation, targetRotation, currentSpeed/maxSpeed);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(tiltAngleMin, _tankBody.rotation.eulerAngles.y, _tankBody.rotation.eulerAngles.z);
            _tankBody.rotation = Quaternion.Lerp(_tankBody.rotation, targetRotation, currentSpeed/maxSpeed);
        }

        if (currentSpeed > 1 && decelerationInput > 0)
        {
            Vector3 brakingForceVector = -_rigidbody.velocity.normalized * brakingForce;
            _rigidbody.AddForce(brakingForceVector, ForceMode.Acceleration);
            Quaternion targetRotation = Quaternion.Euler(-8, _tankBody.rotation.eulerAngles.y, _tankBody.rotation.eulerAngles.z);
            _tankBody.rotation = Quaternion.Lerp(_tankBody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(tiltAngleMin, _tankBody.rotation.eulerAngles.y, _tankBody.rotation.eulerAngles.z);
            _tankBody.rotation = Quaternion.Lerp(_tankBody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        
        if (currentSpeed < 10 && rotationInput > 0)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
        else if (currentSpeed < 10 && rotationInput < 0)
        {
            transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
        }
    }
}

    
  

