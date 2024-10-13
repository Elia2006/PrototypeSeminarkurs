using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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




    
    protected Vector3 newPos;
    protected int prevState = 0;

    //Damage
    protected int health = 50;
    protected Boolean gotHit;

    //Knockback
    protected Vector3 knockback;

    [SerializeField] ParticleSystem AllertEffect;
    [SerializeField] Transform PatrollPoint;

    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(health);
        gotHit = true;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected Vector3 FindPosOnNavMesh(int distance, Vector3 direction, NavMeshAgent agent, Vector3 originPoint)
    {
        NavMeshHit hit;
        Vector3 randomDirection;

        randomDirection = direction * distance;

        NavMesh.SamplePosition(randomDirection + originPoint, out hit, distance, NavMesh.AllAreas);

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

    protected bool IsPlayerInRange(Vector3 playerPosition, LayerMask groundLayer, float sightDistance)
    {
        if(!Physics.Linecast(transform.position, playerPosition, groundLayer) && Vector3.Distance(transform.position, playerPosition) < sightDistance)
        {
            //Allert(allertRadius, PlayerTrans);
            return true;
        }else
        {
            return false;
        }
    }

    public void KnockbackStart()
    {

        knockback = Player.transform.forward / 3;
    }
    
    protected bool KnockbackUpdate(float height)
    {
        NavMeshHit hit;
        knockback *= 0.95f;
        if(Vector3.Distance(new Vector3(), knockback) <= 0.05f)
        {
            knockback = new Vector3();
            return false;
        }
        else
        {
            agent.enabled = false;
            NavMesh.SamplePosition(transform.position + knockback * Time.deltaTime * 100, out hit, 10, NavMesh.AllAreas);
            transform.position = hit.position;
            return true;
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
