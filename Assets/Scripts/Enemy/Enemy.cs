using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Transform playerTrans;
    private UnityEngine.AI.NavMeshAgent agent;
    RaycastHit hit;
    [SerializeField] LayerMask groundLayer;

    private int health = 50;

    //Patrolling
    public int sightRange = 10;
    public int patrollingRange = 10;
    private Vector3 patrollingPos;

    //Attack
    private float attackCooldown;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player");
        playerTrans = player.GetComponent<Transform>();
    }

    void Update()
    {


        Physics.Linecast(transform.position, playerTrans.position, out hit, groundLayer);

        if(hit.transform == null && hit.distance < sightRange)
        {
            agent.destination = playerTrans.position;

        }else if(Vector3.Distance(transform.position, agent.destination) < 2 )
        {
            Patrolling();
        }

        attackCooldown -= Time.deltaTime;
    }

    private void Patrolling()
    {
        NavMeshHit hit;
        Vector3 randomDirection;
        
        randomDirection = Random.insideUnitSphere * patrollingRange;

        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out hit, patrollingRange, NavMesh.AllAreas);

        patrollingPos = hit.position;
        agent.destination = patrollingPos;
    }

    public void Attack()
    {
        
        if(attackCooldown <= 0)
        {
            attackCooldown = 1;
            player.GetComponent<HUD>().TakeDamage(10);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        agent.destination = playerTrans.position;

        if (health <= 0) 
        { 
            Destroy(gameObject);
        }
    }
}
