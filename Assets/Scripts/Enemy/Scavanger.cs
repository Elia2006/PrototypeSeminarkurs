using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scavanger : Enemy
{

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
        if(IsPlayerInRange(Player.transform.position, groundLayer, 30))
        {
            Attack();
        }else
        {     
            Patroll();
        }

        
    }

    private void Attack()
    {
        Collider[] allysArround = Physics.OverlapSphere(transform.position, 20);

        foreach(var ally in allysArround)
        {
            if(ally.gameObject.name == "Scavanger")
            {
                Debug.Log("hello");
            }
        }

        agent.destination = newPos;
        newPos = Player.transform.position;
    }
    
    private void Patroll()
    {
        agent.destination = newPos;

        if(Vector3.Distance(transform.position, newPos) < 2)
        {
            Vector3 tempNewPos = FindPosOnNavMesh(10, Random.insideUnitSphere, agent, transform.position);
            if(tempNewPos != new Vector3(0, 0, 0))
            {
                newPos = tempNewPos;
            }
            
        }
    }
}
