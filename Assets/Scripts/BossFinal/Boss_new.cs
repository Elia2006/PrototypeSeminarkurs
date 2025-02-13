using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_new : MonoBehaviour
{
    [SerializeField] Transform Player;

    //Flamethrower
    [SerializeField] GameObject fire;
    [SerializeField] Transform flamethrowerEnd;

    //Missiles
    [SerializeField] GameObject Missile;
    [SerializeField] Transform MissileEnd;

    //Maschine Gun
    [SerializeField] Transform maschineGunTarget;
    private LineRenderer laser;
    private Transform gunEnd;
    [SerializeField] GameObject maschineGunProjectile;
    [SerializeField] LayerMask groundLayer;

    //BossStomp
    [SerializeField] GameObject BossStomp;


    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        gunEnd = GameObject.Find("lower arm L_end").transform;

        StartCoroutine(PickAttack());
    }

    // Update is called once per frame
    void Update()
    {
        TurnTowardsPlayer();
        GoTowardsPlayer();

    }

    IEnumerator PickAttack()
    {
        while(true)
        {
            int attack = Random.Range(0, 3);
            switch(attack)
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

            yield return new WaitForSeconds(5);

        }
    }

    private void TurnTowardsPlayer()
    {
        var lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.01f);

        transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void GoTowardsPlayer()
    {
        if(Vector3.Distance(transform.position, Player.position) > 20)
        {
            transform.position += transform.forward * Time.deltaTime;
        }
    }

    IEnumerator Flamethrower()
    {
        for(int i = 0; i < 20; i++)
        {
            flamethrowerEnd.LookAt(Player.transform);

            float rand = 1 / Vector3.Distance(transform.position, Player.position) * 50;
            flamethrowerEnd.rotation *= Quaternion.Euler(Random.Range(-rand, rand), Random.Range(-rand, rand), 0);

            Instantiate(fire, flamethrowerEnd.position, flamethrowerEnd.rotation);  

            yield return new WaitForSeconds(0.7f);
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
                StartCoroutine(MoveMissiles(temp.transform, 1 + x + y * 4));
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

        for(int i = 0; i < 40; i++)
        {
            Instantiate(maschineGunProjectile, gunEnd.position, Quaternion.LookRotation(maschineGunTarget.position - gunEnd.position, Vector3.up));
            yield return new WaitForSeconds(0.1f);
        }

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
        for(float i = 0; i < 1; i += 0.01f)
        {
            transform.position += new Vector3(0, Mathf.Cos(i * Mathf.PI) * 0.2f, 0);

            transform.position += (Player.position + Vector3.up * 2 - transform.position).normalized * 0.1f;



            yield return new WaitForSeconds(0.01f);
        }
        

        for(int i = 0; i < 72; i++)
        {
            GameObject nextCicle = Instantiate(BossStomp, transform.position, Quaternion.Euler(0, i * 5, 0));
            nextCicle.GetComponent<BossStomp>().lifeCicle = 20;
        }

        yield return null;
    }
}