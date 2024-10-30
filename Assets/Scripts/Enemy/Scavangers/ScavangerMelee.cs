using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScavangerMelee : Scavanger
{
    [SerializeField] AttackCollider AttackCollider;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        agent = GetComponent<NavMeshAgent>();
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.isStopped = false;

        if(IsPlayerInRange(Player.transform.position, groundLayer, 30))
        {
            if(Attack())
            {
                if(AttackCollider.colliding && AttackCollider.collider != null && AttackCollider.collider.gameObject.CompareTag("Player"))
                {
                    agent.isStopped = true;
                    if(attackCooldown < Time.time)
                    {
                        Player.GetComponent<HUD>().TakeDamage(10);
                        attackCooldown = Time.time + 0.5f;
                    }
                }
            }
        }else
        {     
            Patroll();
        }
    }
}
