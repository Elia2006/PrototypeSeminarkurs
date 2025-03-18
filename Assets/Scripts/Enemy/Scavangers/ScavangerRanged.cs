using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScavangerRanged : Scavanger
{

    [SerializeField] GameObject speer;
    [SerializeField] Transform bulletOrigin;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        health = 30;

        agent = GetComponent<NavMeshAgent>();
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated){
            agent.isStopped = false;

            if(IsPlayerInRange(Player.transform.position, 30))
            {
                if(Attack())
                {
                    var lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.05f);
                    transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));

                    float distance = Vector3.Distance(transform.position, Player.transform.position);
                    if(distance < 10)
                    {
                        agent.isStopped = true;

                        if(attackCooldown < Time.time){
                            attackCooldown = Time.time + 2;
                            Instantiate(speer, bulletOrigin.position, transform.rotation);
                        }
                    }
                }
            }else
            {     
                Patroll();
            }
        }
    }
}
