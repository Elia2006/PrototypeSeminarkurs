using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScavangerRanged : Scavanger
{

    [SerializeField] GameObject speer;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        agent = GetComponent<NavMeshAgent>();
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.isStopped = false;

        if(IsPlayerInRange(Player.transform.position, groundLayer, 30))
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
                        var ISpeer = Instantiate(speer, transform.position, transform.rotation);
                        ISpeer.GetComponent<Speer>().velocityY = distance * 2.2f;
                    }
                }
            }
        }else
        {     
            Patroll();
        }
    }
}
