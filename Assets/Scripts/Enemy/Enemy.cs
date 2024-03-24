using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected GameObject Player;
    protected NavMeshAgent agent;
    public LayerMask groundLayer;
    protected int speed;
    
    protected int patrollingRange = 20;
    protected float sightDistance = 30;
    protected float allertDistance = 40;




    private int health = 50;
    protected Vector3 newPos;
    protected int prevState = 0;

    [SerializeField] ParticleSystem AllertEffect;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected Vector3 FindPosOnNavMesh(int distance, Vector3 direction)
    {
        NavMeshHit hit;
        NavMeshPath path;
        Vector3 randomDirection;
        float loopPrevent = 1;

        path = new NavMeshPath();

        //Problematisch weil loop, wenn sich Programm irgendwie aufhängt liegts wahrscheinlich hierdran
        do{
            randomDirection = direction * distance * loopPrevent;
            loopPrevent -= 0.1f;

            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out hit, distance, NavMesh.AllAreas);

            NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);
        }while(!(path.status == NavMeshPathStatus.PathComplete));


        return hit.position;
    }

    protected bool IsPlayerInRange(Transform PlayerTrans, LayerMask groundLayer, float sightDistance, float allertRadius)
    {
        RaycastHit hit;
        RaycastHit[] hits;
        
        Physics.Linecast(transform.position, PlayerTrans.position, out hit, groundLayer);
        if(hit.transform == null && Vector3.Distance(transform.position, PlayerTrans.position) < sightDistance)
        {
            if(prevState == 0)
            {
                AllertEffect.Play();
                hits = Physics.SphereCastAll(transform.position, allertRadius, Vector3.up, 0);
                foreach(RaycastHit i in hits)
                {
                    if(i.transform.GetComponent<Enemy>() != null){
                        i.transform.GetComponent<Enemy>().newPos = PlayerTrans.position;
                    }
                }
            }


            return true;
        }else
        {
            return false;
        }
    }

    protected Vector3 PatrollingState(int patrollingRange)
    {
        if(Vector3.Distance(transform.position, newPos) < 3)
        {
            newPos = FindPosOnNavMesh(patrollingRange, Random.insideUnitSphere);
            prevState = 0;

        }
        return newPos;
    }

    protected Vector3 AttackState(Transform PlayerTrans)
    {
        newPos = PlayerTrans.position;

        prevState = 1;

        return newPos;
    }

    protected float AgentSpeed()
    {
        if(prevState == 1)
        {
            return 1;
        }else
        {
            return 0.5f;
        }
    }
}
