using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public bool colliding;
    public Collider coll;

    public void OnTriggerEnter(Collider other)
    {
        colliding = true;
        coll = other;  
    }
    public void OnTriggerExit(Collider other)
    {
        colliding = false;
    }
}
