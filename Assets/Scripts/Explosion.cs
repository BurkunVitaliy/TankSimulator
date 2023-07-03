using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float Radious, Force;

    
    public void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position,Radious);
        
        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(Force,transform.position, Radious);
            }
        }

    }
    
}
