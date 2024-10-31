using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public Animator anim;
    public GameObject Player;
    public GameObject MissileArm;
    public GameObject GrenadeArm;
    public Rigidbody BossRB;
    [SerializeField] Image bossHealthBar;
    public GameObject BossHealth;

    public GameObject DamageAura;

    public GameObject MissilePrefab;
    public GameObject Phase2MissilePrefab;
    public GameObject GrenadePrefab;
    public GameObject Phase2GrenadePrefab;
    public GameObject HomingMissilePrefab;

    bool missile = false;
    bool grenade = false;
    bool phase2missile = false;
    bool phase2grenade = false;
    bool trackingmissiles = false;

    

    #region Variables
    public float throwForce;
    public float throwUpwardForce;
    bool wait = false;
    int rand = 0;
    float bossMoveSpeed = 20f;
    public float bossHealth = 1000;
    public float bossMaxHealth;
    
    #endregion

    private void Awake()
    {
    
        BossHealth.SetActive(true);

    }
    // Start is called before the first frame update
    void Start()
    {
        BossRB = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        anim.SetBool("Attacking", true);
        bossMaxHealth = bossHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthBar.fillAmount = Mathf.Clamp(bossHealth / bossMaxHealth, 0, 1); 
       

        LookAtPlayer();
        
        anim.SetFloat(("distance"), (transform.position - Player.transform.position).magnitude);
        
        
        if (anim.GetFloat("distance") > 9 && anim.GetFloat("distance") < 45 )
        {
            DamageAura.SetActive(false);
            if (wait == false) { InitializeAttack(); }
        } else if((transform.position - Player.transform.position).magnitude > 45)
        {
            //Funktioniert, sieht aber schlecht aus und ist nicht sauber
            Vector3 towards = new Vector3 (Player.transform.position.x, 3.24f, Player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, towards, bossMoveSpeed * Time.deltaTime);
        } else if(anim.GetFloat("distance") < 9)
        {
            //an der Damage Aura noch arbeiten
            DamageAura.SetActive(true);
            Vector3 away = new Vector3(Player.transform.position.x , transform.position.y , Player.transform.position.z/2);
            Debug.Log(transform.position - away);
            transform.position = Vector3.MoveTowards(transform.position, away, -1*bossMoveSpeed);
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
                    phase2missile = true;
                    break;
                case 4:
                    anim.SetTrigger("Phase 2 Grenade"); //grenade, that leaves behind a AOE field
                    phase2grenade = true;
                    break;
                case 5:
                    anim.SetTrigger("Tracking Missiles"); // shoots homing missiles after a delay
                    trackingmissiles = true;
                    break;
            }

            if (missile) 
            {
                missile = false;
                //in ne andere methode, um so nen burst von 6 zu spawnen jeweils
                Vector3 missileSpawnPos = MissileArm.transform.position;
            
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

                
            } else if (phase2missile)
            {
                phase2missile = false;
                Vector3 missileSpawnPos = MissileArm.transform.position;

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
            } else if(phase2grenade)
            {
                phase2grenade = false;
                Vector3 grenadeSpawnPos = GrenadeArm.transform.position;
                Vector3 grenadeDir = transform.forward;
                Vector3 spawnPos = grenadeSpawnPos + grenadeDir;
                GameObject Grenade = Instantiate(Phase2GrenadePrefab, spawnPos, GrenadeArm.transform.rotation);
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
            } else if(trackingmissiles)
            {
                //spawn tracking missiles
            }
        }

            anim.SetBool("Attacking", true);
            



            

        }
    
    
    public void Unstoppable()
    {
        Vector3 targetPos = transform.position - Player.transform.position / 2;
        Debug.Log(targetPos);
        
        while ((transform.position - targetPos).magnitude > 2)
        {
            transform.position = targetPos * bossMoveSpeed * Time.deltaTime;
        }
        

    }

    public void BossTakeDamage(int amount)
    {
        bossHealth -= amount;
        if(bossHealth <= 0)
        {
            BossDeath();
        }
    }

    public void BossDeath()
    {
        BossHealth.SetActive(false);
        Destroy(gameObject);
        //Todeseffekt kommt auch hier rein
    }

}


