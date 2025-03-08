using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBoy : Enemy
{
    [SerializeField] Transform[] patrollPoints;

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(transform.position, agent.destination) < 5)
        {
            i++;
            
            if(i >= patrollPoints.Length)
            {
                i = 0;
            }
            agent.destination = patrollPoints[i].position;
        }
    }
}
