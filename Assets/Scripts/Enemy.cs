using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private UnityEngine.AI.NavMeshAgent agent;

    public int health = 50;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        

        RaycastHit hit;


        Physics.Linecast(transform.position, player.position, out hit);

        if(hit.transform.CompareTag("Player") && hit.distance < 20)
        {
            agent.destination = player.position;
        }
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
