using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject tank;
    public float bulletVelosity = 100f, rotationSpeed = 1f, recoilForce = 500f, rotateAngle = -10f, pos = 2;
    public ParticleSystem departureParticles;
    public float movementTimeForRot = 0.5f,movementTimeForPos = 0.5f;
    
    private GameObject _tankBody;
    private Vector3 startPos, target;
    private Vector3 targetPos;
    private float elapsedTime = 0f;

    private void Start()
    {
        _tankBody = tank.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Bullet.isBulletWhole)
        {
            departureParticles.Play();
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletVelosity , ForceMode.Force);
            
            StartCoroutine(RotateObject());
            StartCoroutine(MoveObject());
            Bullet.isBulletWhole = false;
        }
    }

    private IEnumerator RotateObject()
    {
        elapsedTime = 0f;
        while (elapsedTime < movementTimeForRot)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / rotationSpeed);
            Quaternion newRotation = Quaternion.Lerp(_tankBody.transform.rotation, 
                Quaternion.Euler(rotateAngle , 
                _tankBody.transform.rotation.eulerAngles.y, 
                _tankBody.transform.rotation.eulerAngles.z),
                t);
            _tankBody.transform.rotation = newRotation;
            yield return null;
        }
    }
    
    private IEnumerator MoveObject()
    {
        elapsedTime = 0f;
        while (elapsedTime < movementTimeForPos)
        {
            elapsedTime += Time.deltaTime;
            tank.GetComponent<Rigidbody>().AddForce(-transform.forward * recoilForce,ForceMode.Impulse );
            yield return null;
        }
    }
}
