using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Enemy
{
    



    //Attack
    private float jumpCooldown;
    private float lerp;
    private Vector3 oldPos;
    private Vector3 jumpDestination;
    [SerializeField] GameObject JumpHit;
    [SerializeField] Vector3 patrollPoint;

    private Quaternion jumpHitRotation;

    //Attack
    enum AttackState {Charge, Retreat, JumpStart, Jumping};
    [SerializeField] GameObject Hit;
    private AttackState attackState;
    private float chargeTimer;

    //Retreat
    private int dodge = 3;

    //ANIMATION

    //get direction
    private Vector3 lastPos;
    private Vector3 currentDirection;
    [SerializeField] Transform RotationFix;


    void Start()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        speed = 5;
        patrollingRange = 20;
        health = 300;

        sightDistance = 30;
        allertDistance = 60;

        agent.updateUpAxis = false;
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
            if(IsPlayerInRange(Player.transform.position, groundLayer, sightDistance) || lerp > 0)
            {
                Attack();
            }else
            {     
                Patroll();
            }
            gotHit = false;
        }

        //Rotation();


        agent.speed = speed * speedMultiplier;
    }

    public void Attack()
    {
        speedMultiplier = 2;

        float distance = Vector3.Distance(transform.position, Player.transform.position);

        //Rotate towards Player
        agent.updateRotation = false;
        var lookRotation = Quaternion.LookRotation(Player.transform.position + Player.GetComponent<PlayerMovement>().direction * Vector3.Distance(transform.position, Player.transform.position) * 3 - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.05f);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));


        //get attackState

        if(lerp > 0)
        {
            attackState = AttackState.Jumping;
            lerp -= Time.deltaTime;
            if(knockback != new Vector3())
            {
                attackState = AttackState.Charge;
                agent.enabled = true;
                chargeTimer = Time.time + 4;
                lerp = 0;
            }
            else if(lerp <= 0)
            {
                Instantiate(JumpHit, jumpDestination, jumpHitRotation);
                attackState = AttackState.Charge;
                agent.enabled = true;
                chargeTimer = Time.time + 4;
            }

        }
        else if(chargeTimer > Time.time)
        {
            attackState = AttackState.Charge;
        }
        else if(jumpCooldown < Time.time && distance < 15)
        {
            attackState = AttackState.JumpStart;
            jumpCooldown = Time.time + 10;
            lerp = 1;
        }
        else
        {
            attackState = AttackState.Retreat;
        }
        
        
        //execute attack
        if(attackState == AttackState.Jumping)
        {
            transform.position = Vector3.Lerp(jumpDestination, oldPos, lerp);
            transform.position += new Vector3(0, Mathf.Sin(lerp * Mathf.PI) * 4, 0);
            
        }
        else if(attackState == AttackState.JumpStart)
        {
            oldPos = transform.position;

            RaycastHit hit;

            Physics.Raycast(Player.transform.position + Player.GetComponent<PlayerMovement>().direction * 10, Vector3.down, out hit, Mathf.Infinity, groundLayer);

            jumpHitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            jumpDestination = hit.point + Vector3.up; //wegen eigener Größe des Charakters
            newPos = jumpDestination;

            agent.enabled = false;            
        }
        else if(attackState == AttackState.Charge)
        {
            newPos = Player.transform.position;
            agent.destination = newPos;

            RaycastHit hit;

            Physics.Raycast(transform.position, transform.forward, out hit, 4);
            Debug.DrawLine(transform.position, transform.position + transform.forward * 4);

            if(hit.transform != null && hit.transform.CompareTag("Player") && attackCooldown < Time.time)
            {
                Physics.Raycast(transform.position + transform.forward * 4, Vector3.down, out hit, Mathf.Infinity, groundLayer);

                Instantiate(Hit, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

                attackCooldown = Time.time + 1;
            }
        }
        else if(attackState == AttackState.Retreat)
        {
            if(distance < 13)
            {
                do{
                    agent.destination = FindPosOnNavMesh(1, (transform.position - Player.transform.position).normalized, agent, transform.position);
                }while(agent.destination == new Vector3(0, 0, 0));
            }
            else if(distance > 15)
            {
                agent.destination = Player.transform.position;
            }
            else
            {
                if(Random.Range(0, Time.deltaTime * 1000) > 1)
                {
                    dodge *= -1;
                }

                agent.destination = transform.position + transform.right * dodge;
            }
        }
        



    }
    private void Patroll()
    {
        agent.updateRotation = true;

        speedMultiplier = 1;
        if(Vector3.Distance(transform.position, newPos) < 2 && waitTime < Time.time)
        {
            waitTime = Time.time + 3;
            do{
                newPos = FindPosOnNavMesh(patrollingRange, Random.insideUnitSphere, agent, patrollPoint);
            }while(newPos == new Vector3(0, 0, 0));

        }
        else if(waitTime < Time.time)
        {         
            agent.destination = newPos;
        }
    }
    private void Rotation()
    {
        currentDirection = transform.position - lastPos;

        lastPos = transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(currentDirection, Vector3.up);

        targetRotation = Quaternion.Lerp(targetRotation, Quaternion.Euler(Vector3.up), 0.7f);

        RotationFix.rotation = targetRotation;

        Debug.DrawLine(transform.position, transform.position + currentDirection * 1000);

        //RotationFix.rotation = Quaternion.Euler(currentDirection.z * 1000, 90, -currentDirection.x * 1000);

    }

}
