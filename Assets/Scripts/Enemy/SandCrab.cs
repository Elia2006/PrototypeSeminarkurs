using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class SandCrab : Enemy
{
    private Transform Cam;
    [SerializeField] AttackCollider attackCollider;

    [SerializeField] Animator anim;

    //Audio
    [SerializeField] AudioSource bite;

    
    void Start()
    {
        Player = GameObject.Find("Player");
        Cam = GameObject.Find("PlayerCamera").transform;
        agent = GetComponent<NavMeshAgent>();
        health = 100;
    }

    void Awake()
    {

    }

    void Update()
    {
        if(isActivated){
            Vector3 sight = (transform.position - Player.transform.position).normalized + Cam.forward;
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            if(distance < 5)
            {
                continueCharge = true;
            }
            else if(!IsPlayerInRange(Player.transform.position, 40))
            {
                continueCharge = false;
            }

            bool isInSight = Mathf.Abs(sight.x) > 1 || Mathf.Abs(sight.y) > 1 || Mathf.Abs(sight.z) > 1;

            if(!isInSight && distance < 40 || continueCharge)
            {
                agent.isStopped = false;
                anim.SetBool("isHiding", false);
                Attack();

            }else
            {
                agent.isStopped = true;
                anim.SetBool("isHiding", true);
            }

        }
    }


    private void Attack()
    {
        agent.destination = Player.transform.position;

        if(attackCooldown < Time.time && attackCollider.coll != null && attackCollider.coll.gameObject.CompareTag("Player"))
        {
            attackCooldown = Time.time + 2;
            Player.GetComponent<HUD>().TakeDamage(10, 1, transform.position, 0.2f);
            bite.Play();
        }

        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if(distance < 3)
        {
            agent.isStopped = true;

            //turn towards Player
            var lookRotation = Quaternion.LookRotation(Player.transform.position + Player.GetComponent<PlayerMovement>().direction * Vector3.Distance(transform.position, Player.transform.position) * 3 - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.05f);


        }else
        {
            agent.isStopped = false;
        }
    }


}

