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
        allertDistance = 60;
    }
    private void Awake()
    {
        newPos = transform.position;

    }

    void Update()
    {

        if(IsPlayerInRange(Player.transform, groundLayer, sightDistance, allertDistance))
        {          

            agent.destination = FindAttackPos();
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
    private Vector3 FindAttackPos()
    {
        Vector3 attackPos = new Vector3();
        if(Vector3.Distance(transform.position, attackPos) < 1)
        {
            attackPos = FindPosOnNavMesh(10, transform.forward);
        }

        return attackPos;
    }

}
