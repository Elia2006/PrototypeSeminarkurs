using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator anim;
    public GameObject Player;
    public GameObject MissileArm;
    public GameObject GrenadeArm;
    public Rigidbody BossRB;

    public GameObject DamageAura;

    public GameObject MissilePrefab;
    public GameObject GrenadePrefab;

    bool missile = false;
    bool grenade = false;

    #region State Machine Variables
   

    #endregion

    #region Variables
    public float throwForce;
    public float throwUpwardForce;
    bool wait = false;
    int rand = 0;
    float bossMoveSpeed = 20f;
    #endregion

    //private void Awake()
    //{
    //    StateMachine = new StateMachine();
    //    grenadeState = new BossGrenadeState(this, StateMachine);
    //    damageAuraDashState = new BossDamageAuraDashState(this, StateMachine);
    //    missileState = new BossMissileState(this, StateMachine);

    //}
    // Start is called before the first frame update
    void Start()
    {
        BossRB = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        anim.SetBool("Attacking", true);

        //StateMachine.Initialize(grenadeState);
    }

    // Update is called once per frame
    void Update()
    {
        //StateMachine.CurrentState.FrameUpdate();

        LookAtPlayer();
        //animator.SetFloat("Speed", BossSpeed);
        anim.SetFloat(("distance"), (transform.position - Player.transform.position).magnitude);
        
        //muss hier noch ein if für die Distanz zum Player, so dass er ggf moved reinpacken
        if (anim.GetFloat("distance") > 10 && anim.GetFloat("distance") < 45 )
        {
            if (wait == false) { InitializeAttack(); }
        } else if((transform.position - Player.transform.position).magnitude > 45)
        {
            Debug.Log("Unstoppable");
            Unstoppable();
        }
            
    }

    private void FixedUpdate()
    {
        
    }

    void LookAtPlayer()
    {
        transform.LookAt(Player.transform.position);
        GrenadeArm.transform.LookAt(Player.transform.position);
        MissileArm.transform.LookAt(Player.transform.position);
    }

    public void InitializeAttack()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        Debug.Log("Attacking" + anim.GetBool("Attacking"));
        if (anim.GetBool("Attacking"))
        {
            anim.SetBool(("Attacking"), false);
            

            if (!anim.GetBool("Phase2")) rand = UnityEngine.Random.Range(0, 2);
            if (anim.GetBool("Phase2")) rand = UnityEngine.Random.Range(2, 6);

            Debug.Log("rand " + rand);


            switch (rand)
            {
                case 0:
                    anim.SetTrigger("Missile"); // standard Missile Attack
                    Debug.Log("MissileTrigger");
                    missile = true;
                    wait = true;
                    break;
                case 1:
                    anim.SetTrigger("Grenade"); // standard grenade attack
                    Debug.Log("GrenadeTrigger");
                    grenade = true;
                    wait = true;
                    break;
                case 2:
                    anim.SetTrigger("Dash"); //dashes a short distance 
                    wait = true;
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

            if (missile) 
            {
                missile = false;
                Vector3 missileSpawnPos = MissileArm.transform.position;
                //nicht wirklich die dir
                Vector3 missileDir = MissileArm.transform.forward;
                Vector3 spawnPos = missileSpawnPos + missileDir;
                GameObject Missile = Instantiate(MissilePrefab, MissileArm.transform.position, MissileArm.transform.rotation);
                Rigidbody MissileRb = Missile.GetComponent<Rigidbody>();

                Vector3 forceDirection = GrenadeArm.transform.position - Player.transform.position;

                RaycastHit hit;

                if (Physics.Raycast(MissileArm.transform.position, Player.transform.position, out hit, 50f))
                {
                    forceDirection = (hit.point - MissileArm.transform.position).normalized;
                }

                Vector3 forceToAdd = forceDirection;

                MissileRb.AddForce(forceToAdd);

                yield return new WaitForSeconds(3);
                wait = false;
            } else if (grenade)
            {
                grenade = false;
                Vector3 grenadeSpawnPos = GrenadeArm.transform.position;
                Vector3 grenadeDir = transform.forward;
                Vector3 spawnPos = grenadeSpawnPos + grenadeDir;
                GameObject Grenade = Instantiate(GrenadePrefab, spawnPos, GrenadeArm.transform.rotation);
                Rigidbody grenadeRb = Grenade.GetComponent<Rigidbody>();

                Vector3 forceDirection = GrenadeArm.transform.position - Player.transform.position;

                RaycastHit hit;

                if (Physics.Raycast(GrenadeArm.transform.position, GrenadeArm.transform.forward, out hit, 50f))
                {
                    forceDirection = (hit.point - GrenadeArm.transform.position).normalized;
                }

                Vector3 forceToAdd = forceDirection * throwForce + GrenadeArm.transform.up * throwUpwardForce;

                grenadeRb.AddForce(forceToAdd, ForceMode.Impulse);
                yield return new WaitForSeconds(3);
                wait = false;

                
            }
        }

            anim.SetBool("Attacking", true);
            



            

        }
    //funktioniert noch nicht
    public void Unstoppable()
    {
        Vector3 targetPos = transform.position - Player.transform.position / 2;
        while ((transform.position - targetPos).magnitude < 2)
        {
            transform.position = targetPos * bossMoveSpeed * Time.deltaTime;
        }

    }

}

    #region BossAttacks



    //public void Missile ()
    //{
    //    StartCoroutine(MissileAttack());
    //}

    //IEnumerator MissileAttack ()//int counter
    //{
        
    //   // yield return new WaitForSeconds(delay);
    //    //if (counter > 0)
    //    //{
    //    //    StartCoroutine(MissileAttack(counter - 1));
    //    //}
    //    //yield return new WaitForSeconds(5);
    //    //anim.SetBool("Attacking", true);
    //}

    //void Grenade ()
    //{
    //    StartCoroutine(GrenadeAttack());
    //}

    //IEnumerator GrenadeAttack () 
    //{
        
    //    //yield return new WaitForSeconds(5);
    //    //anim.SetBool("Attacking", true);
    //}

    //alle Attacken in ein Skript?
    //void Dash ()
    //{

    //}

    /*void Phase2Missiles ()
    {

    }

    void Phase2Grenade ()
    {

    }

    void TrackingMissiles ()
    {

    }*/

    #endregion 
    /*void PlayerTooNear()
    {
       // DamageAura.SetActive(true);
        //Dash();

    }*/

    #region Triggers

    

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }
        
    #endregion

