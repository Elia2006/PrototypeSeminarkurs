using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss_new : Enemy
{
    //Flamethrower
    [SerializeField] GameObject fire;
    [SerializeField] Transform flamethrowerEnd;
    [SerializeField] FlamethrowerTarget flamethrowerTarget;

    //Missiles
    [SerializeField] GameObject Missile;
    [SerializeField] Transform MissileEnd;

    //Maschine Gun
    [SerializeField] Transform maschineGunTarget;
    private LineRenderer laser;
    private Transform gunEnd;
    [SerializeField] GameObject maschineGunProjectile;

    //BossStomp
    [SerializeField] GameObject BossStomp;
    [SerializeField] SpiderAnimation leg1;
    [SerializeField] SpiderAnimation leg2;

    //health
    [SerializeField] Image healthBar1;
    [SerializeField] Image healthBar2;
    private readonly float maxHealth = 600;

    [SerializeField] GameObject BossHealth;
    [SerializeField] Collider bossSpawnColl;
    public bool isReset = true;

    //Audio
    [SerializeField] AudioSource maschineGunSound;
    [SerializeField] AudioSource impactSound;


    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        gunEnd = GameObject.Find("lower arm L_end").transform;
        Player = GameObject.Find("Player");

        health = 600;

        disolveSpeed = 0.05f;

        isActivated = false;
    }

    public void StartBoss()
    {
        StartCoroutine(ExecuteAttack());
        isReset = false;
        isActivated = true;
    }

    public void ResetBoss()
    {
        bossSpawnColl.enabled = true;
        StopAllCoroutines();
        transform.localPosition = new Vector3(0, 0, 0);
        isActivated = false;
        health = 600;
        maschineGunSound.mute = true;
        isReset = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (isActivated)
        {
            PickAttack();
            TurnTowardsPlayer();
            GoTowardsPlayer();
            HealthBar();
            continueCharge = true;
        }
        else
        {
            health = 600;
            GetComponent<LineRenderer>().enabled = false;
            BossHealth.SetActive(false);
            continueCharge = false;
            
            try
            {
                Destroy(GameObject.Find("FireGround(Clone)"));
            }catch
            {
            }
        }
    }

    IEnumerator ExecuteAttack()
    {
        while(true)
        {
            foreach(int i in PickAttack())
            {
                //int b = 3;
                switch(i)
                {
                    case 0:
                        yield return StartCoroutine(Flamethrower());
                        break;
                    case 1:
                        yield return StartCoroutine(Missiles());
                        break;
                    case 2:
                        yield return StartCoroutine(MaschineGun());
                        break;
                    case 3:
                        yield return StartCoroutine(Stomp());
                        break;
                }
                yield return new WaitForSeconds(3);
            }

            yield return new WaitForSeconds(3);
        }
    }

    private int[] PickAttack()
    {
        int[] attackOrder = {0, 1, 2, 3};
        for(int i = 0; i < attackOrder.Length; i++)
        {
            int rand = Random.Range(0, 4);
            int z = attackOrder[i];
            attackOrder[i] = attackOrder[rand];
            attackOrder[rand] = z;
        }
        int[] cutAttackOrder = {attackOrder[0], attackOrder[1], attackOrder[2]};

        return cutAttackOrder;
    }

    private void TurnTowardsPlayer()
    {
        var lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.05f);

        transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void GoTowardsPlayer()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) > 15)
        {
            transform.position += transform.forward * Time.deltaTime * 2;
        }
    }

    IEnumerator Flamethrower()
    {
        flamethrowerTarget.StartFlames();
        yield return new WaitForSeconds(3);
        for(int i = 0; i < 40; i++)
        {
            

            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }

    IEnumerator Missiles()
    {
        for(int x = 0; x < 4; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                GameObject temp = Instantiate(Missile, MissileEnd.position, MissileEnd.rotation, transform);
                temp.transform.position += -temp.transform.right * (0.23f * x);
                temp.transform.position += -temp.transform.up * (0.23f * y);
                StartCoroutine(MoveMissiles(temp.transform, (float)(1 + x + y * 4) / 5f));
            }
        }

        yield return null;
    }

    IEnumerator MoveMissiles(Transform missile, float time)
    {
        for(int i = 0; i < 20;  i++)
        {
            missile.position += missile.forward * 0.04f;
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(time);

        missile.transform.parent = null;

        missile.GetComponent<Missile_new>().enabled = true;

        yield return null;
    }

    IEnumerator MaschineGun()
    {
        laser.enabled = true;
        Coroutine laserC = StartCoroutine(Laser());

        yield return new WaitForSeconds(0.5f);

        maschineGunSound.mute = false;

        for(int i = 0; i < 40; i++)
        {
            Instantiate(maschineGunProjectile, gunEnd.position, Quaternion.LookRotation(maschineGunTarget.position - gunEnd.position, Vector3.up));
            yield return new WaitForSeconds(0.1f);
        }
        maschineGunSound.mute = true;

        StopCoroutine(laserC);
        laser.enabled = false;

        yield return null;
    }

    IEnumerator Laser()
    {
        while(true)
        {
            laser.SetPosition(0, gunEnd.position);
            RaycastHit hit;
            Physics.Raycast(gunEnd.position, maschineGunTarget.position - gunEnd.position, out hit, Mathf.Infinity, groundLayer);
            laser.SetPosition(1, hit.point);

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Stomp()
    {
        leg1.enabled = false;
        leg2.enabled = false;
        for(float i = 0; i < 1; i += 0.01f)
        {
            transform.position += new Vector3(0, Mathf.Cos(i * Mathf.PI) * 0.2f, 0);

            transform.position += (Player.transform.position + Vector3.up * 2 - transform.position).normalized * 0.1f;

            yield return new WaitForSeconds(0.01f);
        }
        impactSound.Play();

        leg1.enabled = true;
        leg1.SetNewPos(leg1.transform.position);
        leg2.enabled = true;
        leg2.SetNewPos(leg2.transform.position);

        for(int i = 0; i < 72; i++)
        {
            GameObject nextCicle = Instantiate(BossStomp, transform.position, Quaternion.Euler(0, i * 5, 0));
            nextCicle.GetComponent<BossStomp>().lifeCicle = 20;
        }

        yield return null;
    }

    private void HealthBar()
    {
        healthBar1.fillAmount = Mathf.Clamp(health/maxHealth,0,1);

        if(healthBar2.fillAmount > healthBar1.fillAmount)
        {
            healthBar2.fillAmount -= Time.deltaTime * 0.04f;
        }else if(healthBar2.fillAmount < healthBar1.fillAmount)
        {
            healthBar2.fillAmount = healthBar1.fillAmount;
        }

        //playerHealthText.text = health + "/" + maxHealth;
    }
}