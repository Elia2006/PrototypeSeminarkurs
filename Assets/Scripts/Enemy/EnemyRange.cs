using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRange : Enemy
{



    private int stopDistance = 10;
    private int attackCooldown = 1;
    

    //Attack
    private float attackTimer;
    [SerializeField] GameObject Projectile;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        speed = 5;
        patrollingRange = 20;
        sightDistance = 30;
        allertDistance = 40;
    }

    void Update()
    {
        agent.enabled = true;
        if(IsPlayerInRange(Player.transform, groundLayer, sightDistance, allertDistance))
        {
            agent.destination = AttackState(Player.transform);
            Attack();
            if(Vector3.Distance(transform.position, Player.transform.position) < stopDistance)
            {
                agent.enabled = false;
            }
        }else
        {
            agent.destination = PatrollingState(patrollingRange);
        }
        agent.speed = speed * AgentSpeed();

        attackTimer -= Time.deltaTime;
    }

    private void Attack()
    {
        transform.LookAt(Player.transform.position);

        if(attackTimer < 0)
        {
            GameObject Bullet = Instantiate(Projectile, transform.position, transform.rotation);
            Destroy(Bullet, 1);
            attackTimer = attackCooldown;
        }
    }

}
