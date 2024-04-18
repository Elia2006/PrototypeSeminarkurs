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
    protected float speedMultiplier;
    
    protected int patrollingRange = 20;
    protected float waitTime = 0;
    protected float sightDistance = 30;
    protected float allertDistance = 40;
    protected float attackCooldown = 0;




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

    protected Vector3 FindPosOnNavMesh(int distance, Vector3 direction, NavMeshAgent agent)
    {
        NavMeshHit hit;
        Vector3 randomDirection;

        randomDirection = direction * distance;

        NavMesh.SamplePosition(randomDirection + transform.position, out hit, distance, NavMesh.AllAreas);

        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(hit.position, path);

        if(path.status == NavMeshPathStatus.PathComplete)
        {
            return hit.position;
        }
        else{
            return new Vector3(0, 0, 0);
        }

        
    }

    protected bool IsPlayerInRange(Transform PlayerTrans, LayerMask groundLayer, float sightDistance, float allertRadius)
    {
        RaycastHit hit;
        
        Physics.Linecast(transform.position, PlayerTrans.position, out hit, groundLayer);
        if(hit.transform == null && Vector3.Distance(transform.position, PlayerTrans.position) < sightDistance)
        {
            //Allert(allertRadius, PlayerTrans);
            return true;
        }else
        {
            return false;
        }
    }

    private void Allert(float allertRadius, Transform PlayerTrans)
    {
        RaycastHit[] hits;
        if(prevState == 0)
        {
            //AllertEffect.Play();
            hits = Physics.SphereCastAll(transform.position, allertRadius, Vector3.up, 0);
            foreach(RaycastHit i in hits)
            {
                if(i.transform.GetComponent<Enemy>() != null){
                    i.transform.GetComponent<Enemy>().newPos = PlayerTrans.position;
                }
            }
        }
    }
}
