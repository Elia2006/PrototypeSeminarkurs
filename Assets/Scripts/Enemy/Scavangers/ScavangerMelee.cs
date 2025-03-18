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

        health = 50;

        agent = GetComponent<NavMeshAgent>();
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated){
            agent.isStopped = false;

            if(IsPlayerInRange(Player.transform.position, 30))
            {
                if(Attack())
                {
                    if(AttackCollider.colliding && AttackCollider.coll != null && AttackCollider.coll.gameObject.CompareTag("Player"))
                    {
                        agent.isStopped = true;
                        if(attackCooldown < Time.time)
                        {
                            Player.GetComponent<HUD>().TakeDamage(10, 2, transform.position, 0.1f);
                            attackCooldown = Time.time + 2;
                        }
                    }
                }
            }else
            {     
                Patroll();
            }
        }
    }
}
