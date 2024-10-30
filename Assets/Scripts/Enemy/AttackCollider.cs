using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public bool colliding;
    public Collider collider;

    public void OnTriggerEnter(Collider other)
    {
        colliding = true;
        collider = other;  
    }
    public void OnTriggerExit(Collider other)
    {
        colliding = false;
    }
}
