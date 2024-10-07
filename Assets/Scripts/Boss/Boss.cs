using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator anim;
    public GameObject Player;

    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        //animator.SetFloat("Speed", BossSpeed);
        anim.SetFloat(("distance"), (transform.position - Player.transform.position).magnitude);
        //muss hier noch ein if für die Distanz zum Player, so dass er ggf moved reinpacken
    }

    void LookAtPlayer()
    {
        transform.LookAt(Player.transform.position);
    }

    private void Attack()
    {
        if (anim.GetBool("Attacking"))
        {
            int rand = 0;

            if (!anim.GetBool("Phase2")) rand = UnityEngine.Random.Range(0, 2);
            if (anim.GetBool("Phase2")) rand = UnityEngine.Random.Range(2, 5);




            switch (rand)
            {
                case 0:
                    anim.SetTrigger("Missile"); // standard Missile Attack
                    break;
                case 1:
                    anim.SetTrigger("Grenade"); // standard grenade attack
                    break;
                case 2:
                    anim.SetTrigger("Dash"); //dashes a short distance 
                    break;
                case 3:
                    anim.SetTrigger("Phase 2 Missiles"); // accelerated Missiles
                    break;
                case 4:
                    anim.SetTrigger("Phase 2 Grenade"); //grenade, that leaves behind a AOE field
                    break;
                case 5:
                    anim.SetTrigger("Tracking Missiles"); // shoots homing missiles after a delay
                    break;
            }

            anim.SetBool(("Attacking"), false);

        }
       
    }
}
