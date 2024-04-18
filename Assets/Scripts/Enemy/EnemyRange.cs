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
    }
    private void Awake()
    {
        newPos = transform.position;
    }

    void Update()
    {
        agent.enabled = true;
        if(IsPlayerInRange(Player.transform, groundLayer, sightDistance, allertDistance))
        {
            Attack();
        }else
        {
            Patroll();
        }
        agent.speed = speed * speedMultiplier;
    }

    private void Attack()
    {
        transform.LookAt(Player.transform.position);
        transform.localRotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));
        Debug.Log("");

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
