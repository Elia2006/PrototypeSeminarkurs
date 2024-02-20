using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRange : Enemy
{
    private GameObject Player;
    private NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer;

    private int patrollingRange = 20;
    private float sightDistance = 30;
    private float allertDistance = 40;

    private int stopDistance = 10;
    private int attackCooldown = 1;
    

    //Attack
    private float attackTimer;
    [SerializeField] GameObject Projectile;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");

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

        attackTimer -= Time.deltaTime;
    }

    private void Attack()
    {
        transform.LookAt(Player.transform.position);

        if(attackTimer < 0)
        {
            Instantiate(Projectile, transform.position, transform.rotation);
            attackTimer = attackCooldown;
        }
    }

}
