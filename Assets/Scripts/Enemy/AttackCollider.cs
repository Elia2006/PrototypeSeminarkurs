using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeCollider : MonoBehaviour
{
    public bool colliding;

    public void OnTriggerEnter(Collider other)
    {
        colliding = true;
    }
    public void OnTriggerExit(Collider other)
    {
        colliding = false;
    }
}
