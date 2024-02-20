using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private int health = 50;
    public Vector3 newPos;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected Vector3 FindRandPos(int patrollingRange)
    {
        NavMeshHit hit;
        NavMeshPath path;
        Vector3 randomDirection;

        path = new NavMeshPath();

        do{
            randomDirection = Random.insideUnitSphere * patrollingRange;

            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out hit, patrollingRange, NavMesh.AllAreas);

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
            hits = Physics.SphereCastAll(transform.position, allertRadius, Vector3.up, 0);
            foreach(RaycastHit i in hits)
            {
                if(i.transform.GetComponent<Enemy>() != null){
                    i.transform.GetComponent<Enemy>().newPos = PlayerTrans.position;
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
            newPos = FindRandPos(patrollingRange);
        }

        return newPos;
    }

    protected Vector3 AttackState(Transform PlayerTrans)
    {
        newPos = PlayerTrans.position;

        return newPos;
    }
}
