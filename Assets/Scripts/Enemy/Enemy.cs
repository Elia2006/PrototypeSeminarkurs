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
    protected Vector3 knockbackOldPos;
    protected Vector3 knockbackNewPos;
    protected float knockbackLerp = 1;

    [SerializeField] ParticleSystem AllertEffect;
    [SerializeField] Transform PatrollPoint;

    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        gotHit = true;

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
        //knockback = Quaternion.LookRotation(transform.position - Player.transform.position, Vector3.up).eulerAngles.normalized * 0.2f;
        knockback = Player.transform.forward * 3;
        knockbackOldPos = transform.position;
        knockbackLerp = 0;
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + knockback, out hit, 10, NavMesh.AllAreas);
        knockbackNewPos = hit.position;
    }
    
    protected void KnockbackUpdate(float height)
    {
        agent.enabled = false;
        
        
        transform.position = Vector3.Slerp(knockbackOldPos, knockbackNewPos + Vector3.up * height, knockbackLerp);
        knockbackLerp += Time.deltaTime * 5;
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
