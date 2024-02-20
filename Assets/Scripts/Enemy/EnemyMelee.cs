using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Enemy
{
    private GameObject Player;
    private NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer;
    private int patrollingRange = 20;

    private float sightDistance = 30;
    private float allertDistance = 40;

    //Attack
    private float attackCooldown;

    void Start()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if(IsPlayerInRange(Player.transform, groundLayer, sightDistance, allertDistance))
        {          

            newPos = AttackState(Player.transform);
        }else
        {      
            newPos = PatrollingState(patrollingRange);
                  
        }
        agent.destination = newPos;

        attackCooldown -= Time.deltaTime;
    }

    public void Attack()
    {
        
        if(attackCooldown <= 0)
        {
            attackCooldown = 1;
            Player.GetComponent<HUD>().TakeDamage(10);
        }
    }

}
