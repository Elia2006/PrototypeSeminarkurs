using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform Cam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject ImpactEffect;
    [SerializeField] Transform GunEnd;
    [SerializeField] Animator anim;
    private float attackCooldown;
    public GameObject Player;

    private float rot;



    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused)
        {
            if (Input.GetButtonDown("Fire1") && attackCooldown < Time.time)
            {
                Shoot();
                attackCooldown = Time.time + 0.3f;
            }
            
        }
    }

    void Shoot() 
    {
        anim.SetTrigger("Shoot");

        RaycastHit hit;

        Physics.Raycast(Cam.position, Cam.forward, out hit);

        if(hit.transform != null)
        {
            GunEnd.LookAt(hit.point);
        }
        else
        {
            GunEnd.LookAt(Cam.position + Cam.forward * 50);
        }
        GunEnd.localRotation *= Quaternion.Euler(-rot, 0, 0);
        

        Instantiate(Projectile, GunEnd.position, GunEnd.rotation);

        muzzleFlash.Play();
        
    }
}
