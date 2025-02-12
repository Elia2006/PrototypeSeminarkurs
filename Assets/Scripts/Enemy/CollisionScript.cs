using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] bool isWeakpoint;
    // Start is called before the first frame update


    public void TakeDamage(int damage)
    {
        if(isWeakpoint)
        {
            enemy.TakeDamage(damage * 2);
        }else
        {
            enemy.TakeDamage(damage);
        }
    }

    public bool GetIsWeakpoint()
    {
        return isWeakpoint;
    }
}
