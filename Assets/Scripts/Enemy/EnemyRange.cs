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
    [SerializeField] GameObject Projectile;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        speed = 5;
        patrollingRange = 20;
        sightDistance = 30;
        allertDistance = 60;

        agent.updateRotation = false;
    }
    private void Awake()
    {
        newPos = transform.position;
    }

    void Update()
    {
        if(knockbackLerp >= 1)
        {
            agent.enabled = true;
            if(IsPlayerInRange(Player.transform, groundLayer, sightDistance, allertDistance))
            {
                Attack();
            }else
            {
                Patroll();
            }
        }else
        {
            agent.enabled = false;/*
            knockback = Vector3.Slerp(knockback, new Vector3(0, 0, 0), Time.deltaTime * 10);
            if(knockback.x + knockback.y + knockback.z < 0.1f)
            {
                knockback = new Vector3(0, 0, 0);
            }
            transform.position += knockback;*/
            
            
            transform.position = Vector3.Slerp(knockbackOldPos, knockbackNewPos + Vector3.up, knockbackLerp);
            knockbackLerp += Time.deltaTime * 5;
        }

        agent.speed = speed * speedMultiplier;
    }

    private void Attack()
    {
        var lookRotation = Quaternion.LookRotation(Player.transform.position + Player.GetComponent<PlayerMovement>().direction * Vector3.Distance(transform.position, Player.transform.position) * 3 - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.05f);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));

        if(attackCooldown < Time.time)
        {
            if(bulletsLeft > 0)
            {
                GameObject Bullet = Instantiate(Projectile, transform.position, transform.rotation);
                Destroy(Bullet, 1);
                bulletsLeft--;
                attackCooldown = Time.time + 0.1f;
                ReloadTime = Time.time + 2;
            }else if(ReloadTime < Time.time)
            {
                bulletsLeft = 20;
            }

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
                newPos = FindPosOnNavMesh(patrollingRange, Random.insideUnitSphere, agent);
            }while(newPos == new Vector3(0, 0, 0));

        }
        else if(waitTime < Time.time)
        {         
            agent.destination = newPos;
        }
    }

}
