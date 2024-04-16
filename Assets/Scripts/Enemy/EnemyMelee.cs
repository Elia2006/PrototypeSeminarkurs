using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Enemy
{
    



    //Attack
    private float attackCooldown;
    private float jumpCooldown;
    private float lerp;
    private Vector3 oldPos;
    private Vector3 jumpDestination;
    [SerializeField] GameObject JumpHit;
    [SerializeField] GameObject Hit;
    private Quaternion jumpHitRotation;


    void Start()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        speed = 10;
        speedMultiplier = 0.5f;
        patrollingRange = 20;

        sightDistance = 50;
        allertDistance = 60;
    }
    private void Awake()
    {
        newPos = transform.position;

    }

    void Update()
    {

        if(IsPlayerInRange(Player.transform, groundLayer, sightDistance, allertDistance))
        {
            Attack();
        }else
        {     
            Patroll();
        }
        agent.speed = speed * speedMultiplier;
    }

    public void Attack()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        
        if(lerp > 0)
        {
            transform.position = Vector3.Lerp(jumpDestination, oldPos, lerp);
            transform.position += new Vector3(0, Mathf.Sin(lerp * Mathf.PI) * 4, 0);
            lerp -= Time.deltaTime * 1.5f;
            if(lerp <= 0)
            {
                Instantiate(JumpHit, jumpDestination, jumpHitRotation);
            }
        }
        else if(distance > 10 && distance < 15)
        {
            lerp = 1; 
            oldPos = transform.position;

            RaycastHit hit;

            Physics.Raycast(Player.transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer);

            jumpHitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            jumpDestination = hit.point + Vector3.up; //wegen eigener Größe des Charakters
            newPos = jumpDestination;
            agent.enabled = false;
        }
        else
        {
            agent.enabled = true;
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
