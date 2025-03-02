using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform Cam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject ImpactEffect;
    [SerializeField] Transform GunEnd;
    private float attackCooldown;
    public GameObject Player;

    //Aim
    private float acuracy = 10;

    //Animation
    [SerializeField] Animator animShoot;
    [SerializeField] Animator animAim;


    //Audio
    [SerializeField] AudioSource shoot;
    [SerializeField] AudioSource aim;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && attackCooldown < Time.time)
        {
            shoot.Play();
            Shoot();
            attackCooldown = Time.time + 0.3f;
        }

        if(Input.GetMouseButton(1))
        {
            animAim.SetBool("IsAiming", true);
            acuracy = 1;

            Player.GetComponent<PlayerMovement>().isAiming = true;
        }else
        {
            animAim.SetBool("IsAiming", false);
            acuracy = 10;
            Player.GetComponent<PlayerMovement>().isAiming = false;
        }
    }

    void Shoot() 
    {
        animShoot.SetTrigger("Shoot");

        RaycastHit hit;

        Physics.Raycast(Cam.position, Cam.forward, out hit);

        if(hit.point.sqrMagnitude > .01f)
        {
            GunEnd.LookAt(hit.point);
        }
        else
        {
            GunEnd.LookAt(Cam.position + Cam.forward * 50);
        }        

        Vector3 spread = new Vector3(Random.Range(acuracy, -acuracy), Random.Range(acuracy, -acuracy), 0);

        Instantiate(Projectile, GunEnd.position, GunEnd.rotation * Quaternion.Euler(spread));

        muzzleFlash.Play();
        
    }
}
