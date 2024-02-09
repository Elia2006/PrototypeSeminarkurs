using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AttackCollider : MonoBehaviour
{
    public Enemy enemyScript;
    public ParticleSystem EnemyHitEffect;
    private float waitBeforeAttack = -2;

    void Update()
    {
        waitBeforeAttack -= Time.deltaTime;
    }

    public void OnTriggerStay(Collider other)
    {
        if(waitBeforeAttack <= -0.5f)
        {
            waitBeforeAttack = 0.5f;
            EnemyHitEffect.Play();
        }else if(waitBeforeAttack < 0 && waitBeforeAttack > -0.5f)
        {
            enemyScript.Attack();
        }
    }
}
