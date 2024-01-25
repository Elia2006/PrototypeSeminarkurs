using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private UnityEngine.AI.NavMeshAgent agent;

    public int health = 50;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0) 
        { 
            Destroy(gameObject);
        }
    }
}
