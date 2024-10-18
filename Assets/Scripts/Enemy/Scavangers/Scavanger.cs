using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Scavanger : Enemy
{
    int allyCount;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        agent = GetComponent<NavMeshAgent>();
        newPos = transform.position;
    }

    // Update is called once per frame

    protected bool Attack()
    {
        Collider[] allysArround = Physics.OverlapSphere(transform.position, 30);
        allyCount = 0;

        foreach(var ally in allysArround)
        {
            Scavanger scavanger = ally.gameObject.GetComponent<Scavanger>();
            if(scavanger != null)
            {
                allyCount++;
            }
        }
        if(allyCount > 2)
        {
            agent.destination = newPos;
            newPos = Player.transform.position;

            foreach(var ally in allysArround)
            {
                Scavanger scavanger = ally.gameObject.GetComponent<Scavanger>();
                if(scavanger != null)
                {
                    scavanger.setNextPatrollPosition(newPos);
                }
            }

            return true;
        }
        else
        {
            newPos = FindPosOnNavMesh(1, (transform.position - Player.transform.position).normalized, agent, transform.position);
            agent.destination = newPos;
            return false;
        }

    }
    
    protected void Patroll()
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

                    if(scavanger != null)
                    {
                        scavanger.setNextPatrollPosition(newPos);
                    }
                }
            }
            
        }
    }

    public void setNextPatrollPosition(Vector3 nextPatrollPos)
    {
        newPos = nextPatrollPos;
    }
}
