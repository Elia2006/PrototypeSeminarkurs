using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private UnityEngine.AI.NavMeshAgent agent;

    public int health = 50;
    public int sightRange = 10;
    public int patrollingRange = 10;

    private Vector3 patrollingPos;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        

        RaycastHit hit;


        Physics.Linecast(transform.position, player.position, out hit);

        if(hit.transform.CompareTag("Player") && hit.distance < sightRange)
        {
            agent.destination = player.position;
            Debug.Log("chasing");
        }else if(Vector3.Distance(transform.position, agent.destination) < 2 )
        {
            Patrolling();
        }
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

    public void TakeDamage(int amount)
    {
        health -= amount;
        agent.destination = player.position;

        if (health <= 0) 
        { 
            Destroy(gameObject);
        }
    }
}
