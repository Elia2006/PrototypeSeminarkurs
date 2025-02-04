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
    private float attackCooldown;
    public GameObject Player;

    private float defXRot;
    private float rot;
    private float rotD;


    // Start is called before the first frame update
    void Awake()
    {
        defXRot = transform.localRotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused)
        {
            if (Input.GetButtonDown("Fire1") && attackCooldown < Time.time)
            {
                Shoot();
                attackCooldown = Time.time + 0.7f;
                rotD = 8;
            }

            transform.localRotation = Quaternion.Euler(defXRot - rot, 0, 0);

            if(rotD > 0 || rot > 0)
            {                
                if(rotD > 0)
                {
                    rotD -= Mathf.Pow(rot * 1, 2) * Time.deltaTime;

                    if(rotD < 0)
                    {
                        rotD = 0;
                    }
                
                }else
                {
                    rotD -= Time.deltaTime;
                }
                rot += rotD;
            }
        }
    }

    void Shoot() 
    {
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
