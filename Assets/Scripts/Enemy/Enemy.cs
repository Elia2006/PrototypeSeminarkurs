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
    public LayerMask dontColliderLayer;
    protected int speed;
    protected float speedMultiplier;
    
    protected int patrollingRange = 20;
    protected float waitTime = 0;
    protected float sightDistance = 30;
    protected float allertDistance = 40;
    protected float attackCooldown = 0;

    public bool goingToLastPos;



    
    protected Vector3 newPos;
    protected int prevState = 0;

    //Damage
    protected int health = 50;
    public Boolean continueCharge;

    //Knockback
    protected Vector3 knockback;

    [SerializeField] ParticleSystem AllertEffect;
    public Transform patrollPoint;

    public bool isActivated = true;

    //Disolve
    protected float disolveSpeed = 0.02f;

    

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        
        if (health <= 0)
        {
            Death();
        }else
        {
            continueCharge = true;
            newPos = Player.transform.position;
            Allert(50);
        }
    }

    protected void Search()
    {
        speedMultiplier = 1;
        agent.destination = newPos;
        if(Vector3.Distance(transform.position, newPos) < 2)
        {
            continueCharge = false;
        }
    }

    protected void Death()
    {
        StopAllCoroutines();

        isActivated = false;

        if(agent != null)
        {
            agent.enabled = false;
        }
        
        transform.GetComponent<Animator>().enabled = false;

        if(transform.Find("Armature") != null)
        {
            Collider[] myCollider = transform.GetComponentsInChildren<Collider>();

            foreach (Collider c in myCollider)
            {
                c.enabled = false;
            }

            GameObject armature = transform.Find("Armature").gameObject;
            Collider[] childCollider = armature.GetComponentsInChildren<Collider>();
            Rigidbody[] childRigidbody = armature.GetComponentsInChildren<Rigidbody>();

            foreach (Collider child in childCollider)
            {
                child.enabled = true;
                child.gameObject.layer = LayerMask.NameToLayer("DontCollide");
            }
            foreach (Rigidbody child in childRigidbody)
            {
                child.isKinematic = false;
            }

            

            if(transform.TryGetComponent<Disolve>(out Disolve disolve))
            {
                StartCoroutine(disolve.StartDisolve(disolveSpeed));
            }

            StartCoroutine(DieSlowly());
        }else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DieSlowly()
    {
        yield return new WaitForSeconds(disolveSpeed * 250);
        Destroy(gameObject);
        yield return null;
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

    protected bool IsPlayerInRange(Vector3 playerPosition, float sightDistance)
    {
        RaycastHit hit;
        if(!Physics.Linecast(transform.position + Vector3.up * 2, playerPosition, out hit, groundLayer) && 
            Vector3.Distance(transform.position, playerPosition) < sightDistance)
        {
            //Allert(allertRadius, PlayerTrans);
            return true;
        }else
        {
            return false;
        }
    }

    protected Quaternion TurnTowardsPlayer(float speed)
    {
        var lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);
        return Quaternion.Euler(new Vector3(0, Quaternion.Lerp(transform.rotation, lookRotation, speed).eulerAngles.y, 0));
    }

    public void KnockbackStart(float strength)
    {

        knockback = Player.transform.forward * strength;
    }
    
    protected bool KnockbackUpdate()
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
            agent.isStopped = true;
            NavMesh.SamplePosition(transform.position + knockback * Time.deltaTime * 100, out hit, 10, NavMesh.AllAreas);
            transform.position = hit.position;
            return true;
        }
        
    }


    protected void Allert(float allertRadius)
    {
        RaycastHit[] hits;

        //AllertEffect.Play();
        hits = Physics.SphereCastAll(transform.position, allertRadius, Vector3.up, 1);
        foreach(RaycastHit i in hits)
        {
            if(i.transform.parent != null)
            {
                if(i.transform.parent.GetComponent<EnemyMelee1>() != null || i.transform.parent.GetComponent<EnemyRange>() != null){
                    i.transform.parent.GetComponent<Enemy>().continueCharge = true;
                    i.transform.parent.GetComponent<Enemy>().newPos = Player.transform.position;
                }
            }
            
        }

    }
}
