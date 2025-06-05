using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRange : Enemy
{

    //Navigation
    private int stopDistance = 10;
    
    //Attack
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform BulletOrigin;
    [SerializeField] Rotate ring1;
    [SerializeField] Rotate ring2;

    //Audio
    [SerializeField] AudioSource shoot;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        speed = 5;
        patrollingRange = 20;
        sightDistance = 30;
        allertDistance = 60;
        health = 60;
    }
    private void Awake()
    {
        newPos = transform.position;
    }

    void Update()
    {
        if(isActivated){
            if(!KnockbackUpdate())
            {
                if (IsPlayerInRange(Player.transform.position, sightDistance))
                {
                    continueCharge = true;
                }
                else
                {
                    continueCharge = false;
                }

                if(continueCharge)
                {
                    Attack();
                    agent.updateRotation = false;
                }else if(chase)
                {
                    Search();
                }else
                {
                    Patroll();
                    agent.updateRotation = true;
                }
            }

            agent.speed = speed * speedMultiplier;
        }else
        {
            continueCharge = false;
        }
    }

    private void Attack()
    {
        chase = true;
        Allert(50);

        var lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);
        BulletOrigin.rotation = Quaternion.Lerp(BulletOrigin.rotation, lookRotation, 0.05f);

        if(attackCooldown < Time.time)
        {
            StopAllCoroutines();
            StartCoroutine(Shoot());
            attackCooldown = Time.time + 7;
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

    IEnumerator Shoot()
    {
        agent.isStopped = true;

        yield return StartCoroutine(AccelelrateRings());

        for(int i = 0; i < 20; i++)
        {
            GameObject Bullet = Instantiate(Projectile, BulletOrigin.position, BulletOrigin.rotation);
            shoot.Play();
            Destroy(Bullet, 1);
            yield return new WaitForSeconds(0.1f);
        }
        
        yield return StartCoroutine(DecelerateRings());

        agent.isStopped = false;


        yield return null;
    }

    IEnumerator AccelelrateRings()
    {
        while(ring1.rotationSpeed < 800)
        {
            ring1.rotationSpeed += 10;
            ring2.rotationSpeed += 10;

            float lerp = (ring1.rotationSpeed - 100) / 700;

            ring1.GetComponent<Renderer>().material.SetFloat("_Lerp", lerp);
            ring2.GetComponent<Renderer>().material.SetFloat("_Lerp", lerp);

            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

    IEnumerator DecelerateRings()
    {
        while(ring1.rotationSpeed > 100)
        {
            ring1.rotationSpeed -= 10;
            ring2.rotationSpeed -= 10;

            float lerp = (ring1.rotationSpeed - 100) / 700;

            ring1.GetComponent<Renderer>().material.SetFloat("_Lerp", lerp);
            ring2.GetComponent<Renderer>().material.SetFloat("_Lerp", lerp);

            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }


    private void Patroll()
    {
        speedMultiplier = 0.5f;
        if(Vector3.Distance(transform.position, newPos) < 2 && waitTime < Time.time)
        {
            waitTime = Time.time + 3;
            
            var checkNewPos = FindPosOnNavMesh(patrollingRange, Random.insideUnitSphere, agent, patrollPoint.position);
            if(checkNewPos != new Vector3(0, 0, 0))
            {
                newPos = checkNewPos;
            }

        }
        else if(waitTime < Time.time)
        {         
            agent.destination = newPos;
        }
    }

}
