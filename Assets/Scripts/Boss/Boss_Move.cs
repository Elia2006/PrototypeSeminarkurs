using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : StateMachineBehaviour
{

    public float BossSpeed = 2.5f;
    public float attackRange = 25f;

    Transform player;
    Rigidbody rb;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        animator.SetBool("Move", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { 
    //{
    //    Vector3 target = new Vector3(player.position.x, rb.position.y, player.position.z);
    //    //Vector3 subtract = target.normalized * 30;
    //    Vector3 newPos = Vector3.MoveTowards(rb.position, target, BossSpeed * Time.fixedDeltaTime);
    //    rb.MovePosition(newPos);

    //    if (Vector3.Distance(player.position, rb.position) <= attackRange)
    //    {
    //        animator.SetBool("Attack", true);
    //        //attackieren, auch so mit verschiedenen Attack-Möglichkeiten
    //    }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Move", false);
    }


}
