using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] bool isWeakpoint;
    // Start is called before the first frame update


    public void TakeDamage(int damage, bool weakpointOverride, bool isCrowbar)
    {
        if(enemy.enabled)
        {
            if(isWeakpoint && !weakpointOverride)
            {
                enemy.TakeDamage(damage * 2, isCrowbar);
            }else
            {
                enemy.TakeDamage(damage, isCrowbar);
            }
        }
    }

    public void Knockback(float strength)
    {
        enemy.KnockbackStart(strength);
    }

    public bool GetIsWeakpoint()
    {
        return isWeakpoint;
    }
}
