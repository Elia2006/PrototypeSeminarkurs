using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Scavanger : Enemy
{
    int allyCount;
    public GameObject scavangerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        agent = GetComponent<NavMeshAgent>();
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerInRange(Player.transform.position, groundLayer, 30))
        {
            Attack();
        }else
        {     
            Patroll();
        }

        
    }

    private void Attack()
    {
        Collider[] allysArround = Physics.OverlapSphere(transform.position, 50);
        allyCount = 0;

        foreach(var ally in allysArround)
        {
            Scavanger scavanger = ally.gameObject.GetComponent<Scavanger>();
            if(scavanger != null)
            {
                allyCount++;
            }
        }
        if(allyCount > 1)
        {
            agent.destination = newPos;
            newPos = Player.transform.position;
        }
        else
        {
            agent.destination = FindPosOnNavMesh(1, (transform.position - Player.transform.position).normalized, agent, transform.position);
        }

    }
    
    private void Patroll()
    {
        agent.destination = newPos;

        if(Vector3.Distance(transform.position, newPos) < 2)
        {
            Vector3 tempNewPos = FindPosOnNavMesh(30, Random.insideUnitSphere, agent, transform.position);
            if(tempNewPos != new Vector3(0, 0, 0))
            {
                newPos = tempNewPos;
                
                //Notify Allys of new Point
                Collider[] allysArround = Physics.OverlapSphere(transform.position, 30);
                foreach(var ally in allysArround)
                {
                    Scavanger scavanger = ally.gameObject.GetComponent<Scavanger>();

                    scavanger.setNextPatrollPosition(newPos);
                }
            }
            
        }
    }

    public void setNextPatrollPosition(Vector3 nextPatrollPos)
    {
        newPos = nextPatrollPos;
    }
}
