using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionParticles;
    public static bool isBulletWhole = true;
    private void OnCollisionEnter(Collision other)
    {
        gameObject.GetComponent<Explosion>().Explode();
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        isBulletWhole = true;
    }
}
