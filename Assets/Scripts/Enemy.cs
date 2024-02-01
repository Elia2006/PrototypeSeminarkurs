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
        

        RaycastHit hit;

        /*
        for(int y = -2; y <= 2; y++)
        {
            for(int x = -2; x <= 2; x++)
            {
                Physics.Raycast(transform.position, transform.forward, out hit);
                Debug.DrawLine(transform.position, hit.point, Color.red);
            }
        }*/

        Physics.Linecast(transform.position, player.position, out hit);

        Debug.Log(hit.distance);

        if(hit.transform.CompareTag("Player") && hit.distance < 10)
        {
            agent.destination = player.position;
        }
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
