using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : StateMachineBehaviour
{

    Transform player;
    Rigidbody rb;

    public Transform initiator;
    public Transform attackPoint;
    public GameObject Grenade;

    public float throwForce = 30f;
    public float throwUpwardForce = 5f;
    public float attackRange = 25f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
