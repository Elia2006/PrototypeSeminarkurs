using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class SandCrab : Enemy
{
    private Transform Cam;
    [SerializeField] AttackCollider attackCollider;
    private Vector3 previousPos;

    private float lerp = 1;
    private int previousState;

    //Attack
    private float attackDuration;
    private bool chargeAttack = false;
    
    void Start()
    {
        Player = GameObject.Find("Player");
        Cam = GameObject.Find("PlayerCamera").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Awake()
    {
        previousPos = transform.position;
    }

    void Update()
    {
        Vector3 sight = (transform.position - Player.transform.position).normalized + Cam.forward;


        if(Vector3.Distance(transform.position, Player.transform.position) < 5 || gotHit)
        {
            chargeAttack = true;
        }
        else if(!IsPlayerInRange(Player.transform.position, groundLayer, 20))
        {
            chargeAttack = false;
        }
        if(!(Mathf.Abs(sight.x) > 1 || Mathf.Abs(sight.y) > 1 || Mathf.Abs(sight.z) > 1) || chargeAttack)
        {
            if(previousState == 1)
            {
                previousState = 0;
                lerp = 0;
            }

            if(lerp > 1){
                
                if(Vector3.Distance(transform.position, Player.transform.position) < 4)
                {
                    agent.isStopped = true;
                }else
                {
                    agent.isStopped = false;
                }

                agent.destination = Player.transform.position;
                previousPos = transform.position;
                Attack();
                
            }
            else
            {
                transform.position = Vector3.Lerp(previousPos - transform.up, previousPos, lerp);
                lerp += Time.deltaTime * 5;
            }

        }else
        {
            if(previousState == 0)
            {
                previousState = 1;
                lerp = 0;
                agent.isStopped = true;
            }

            transform.position = Vector3.Lerp(previousPos, previousPos - transform.up, lerp);
            lerp += Time.deltaTime * 5;


        }

        if(!IsPlayerInRange(Player.transform.position, groundLayer, 45))
        {
            gotHit = false;
        }
    }
    private void Attack()
    {
        if(attackCooldown < Time.time && attackCollider.collider != null && attackCollider.collider.gameObject.CompareTag("Player"))
        {
            attackCooldown = Time.time + 2;
            Player.GetComponent<HUD>().TakeDamage(10);
            Player.GetComponent<PlayerMovement>().Knockback(gameObject.transform);
        }
    }


}

