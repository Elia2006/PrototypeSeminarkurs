using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRange : MonoBehaviour
{
    private GameObject player;
    private Transform playerTrans;
    RaycastHit hit;
    [SerializeField] LayerMask groundLayer;
    private NavMeshAgent agent;

    

    //Patrolling
    [SerializeField] int sightRange = 10;
    [SerializeField] int stopDistance = 10;
    [SerializeField] int patrollingRange = 10;
    [SerializeField] Vector3 patrollingPos;

    //Attack
    private float attackCooldown;
    [SerializeField] GameObject Projectile;


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player");
        playerTrans = player.GetComponent<Transform>();

    }

    void Update()
    {
        agent.isStopped = false;


        float distanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);

        if(!Physics.Linecast(transform.position, playerTrans.position, out hit, groundLayer) && distanceToPlayer < sightRange)
        {
            Debug.Log(hit.transform);
            Debug.DrawLine(transform.position, playerTrans.position);
            if(distanceToPlayer < stopDistance)
            {
                Attack();
                agent.isStopped = true;

            }else
            {
                agent.destination = playerTrans.position;
            }
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

    private void Attack()
    {
        //temporary: Möchte es noch rotation über Zeit machen
        transform.LookAt(playerTrans.position);

        if(attackCooldown < 0)
        {
            Instantiate(Projectile, transform.position, transform.rotation);
            attackCooldown = 2;
        }
        
    }

}
