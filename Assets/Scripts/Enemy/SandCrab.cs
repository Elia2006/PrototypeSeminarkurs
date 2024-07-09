using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class SandCrab : Enemy
{
    [SerializeField] Transform Cam;
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
        if(!IsPlayerInRange(Player.transform.position, groundLayer, 20))
        {
            chargeAttack = false;
        }
        if((!(Mathf.Abs(sight.x) > 1 || Mathf.Abs(sight.y) > 1 || Mathf.Abs(sight.z) > 1) || 
            chargeAttack) && IsPlayerInRange(Player.transform.position, groundLayer, 20))
        {
            if(previousState == 1)
            {
                previousState = 0;
                lerp = 0;
            }

            if(lerp > 1){
                agent.enabled = true;
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
                agent.enabled = false;
            }

            transform.position = Vector3.Lerp(previousPos, previousPos - transform.up, lerp);
            lerp += Time.deltaTime * 5;


        }

        gotHit = false;
    }
    private void Attack()
    {
        if(attackCooldown < Time.time && attackCollider.colliding)
        {
            attackCooldown = Time.time + 3;
            Player.GetComponent<HUD>().TakeDamage(10);
            Player.GetComponent<PlayerMovement>().Knockback(gameObject.transform);
        }
    }
}

