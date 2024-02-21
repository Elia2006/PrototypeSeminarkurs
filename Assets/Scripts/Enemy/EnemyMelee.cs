using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Enemy
{
    
    //Attack
    private float attackCooldown;

    void Start()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        speed = 5;
        patrollingRange = 20;
        sightDistance = 30;
        allertDistance = 40;
        newPos = transform.position;
        Debug.Log(newPos);
    }

    void Update()
    {

        if(IsPlayerInRange(Player.transform, groundLayer, sightDistance, allertDistance))
        {          

            agent.destination = AttackState(Player.transform);
        }else
        {      
            agent.destination = PatrollingState(patrollingRange);
        }
        agent.speed = speed * AgentSpeed();

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