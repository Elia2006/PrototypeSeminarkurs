using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRange : Enemy
{

    //Navigation
    private int stopDistance = 10;
    
    //Attack
    private int bulletsLeft = 20;
    private float ReloadTime;
    private float ChargeTimer;
    [SerializeField] GameObject Projectile;
    private float xRotation;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        speed = 5;
        patrollingRange = 20;
        sightDistance = 30;
        allertDistance = 60;
    }
    private void Awake()
    {
        newPos = transform.position;
    }

    void Update()
    {
        if(!KnockbackUpdate(1f))
        {
            agent.enabled = true;
            if(IsPlayerInRange(Player.transform.position, groundLayer, sightDistance))
            {
                Attack();
                agent.updateRotation = false;
            }else
            {
                Patroll();
                agent.updateRotation = true;
            }
        }

        agent.speed = speed * speedMultiplier;
    }

    private void Attack()
    {
        

        if(attackCooldown < Time.time)
        {
            if(bulletsLeft > 0 && ChargeTimer < Time.time)
            {
                GameObject Bullet = Instantiate(Projectile, transform.position, Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, transform.eulerAngles.z));
                Destroy(Bullet, 1);
                bulletsLeft--;
                attackCooldown = Time.time + 0.1f;
                ReloadTime = Time.time + 2;
            }else if(ReloadTime < Time.time && bulletsLeft <= 0)
            {
                bulletsLeft = 20;
                ChargeTimer = Time.time + 1;
            }

        }
        if(bulletsLeft <= 0)
        {
            TurnTowardsPlayer();
            xRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up).eulerAngles.x;
            agent.isStopped = false;
        }else
        {
            agent.isStopped = true;
        }

        var distance = Vector3.Distance(Player.transform.position, transform.position);
        
        if(distance > stopDistance)
        {
            newPos = Player.transform.position;
            speedMultiplier = 1;
        }else if(distance < stopDistance - 2)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(transform.position - transform.forward, out hit, 5, NavMesh.AllAreas);
            newPos = hit.position;
        }else
        {
            newPos = transform.position;
        }

        agent.destination = newPos;
    }

    private void Patroll()
    {
        speedMultiplier = 0.5f;
        if(Vector3.Distance(transform.position, newPos) < 2 && waitTime < Time.time)
        {
            waitTime = Time.time + 3;
            do{
                newPos = FindPosOnNavMesh(patrollingRange, Random.insideUnitSphere, agent, transform.position);
            }while(newPos == new Vector3(0, 0, 0));

        }
        else if(waitTime < Time.time)
        {         
            agent.destination = newPos;
        }
    }

}
