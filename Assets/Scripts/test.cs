using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    [SerializeField] Transform Player;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.enabled == true)
        {
            agent.destination = Player.position;
        }
        
/*
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(agent.enabled == true)
            {
                agent.enabled = false;
            }else
            {
                agent.enabled = true;
            }
            
        }*/
    }
}
