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
    private GameObject HitTexture;
    [SerializeField] Transform GunEnd;
    private float hitTextureCooldown = 0;
    private float attackCooldown;
    public GameObject Player;

    private float defXRot;
    private float rot;
    private float rotD;


    // Start is called before the first frame update
    void Awake()
    {
        HitTexture = GameObject.Find("HitTexture");
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
                attackCooldown = Time.time/* + 0.7f*/;
                rotD = 8;
            }

            transform.localRotation = Quaternion.Euler(defXRot - rot, 0, 0);

            if(rotD > 0 || rot > 0)
            {
                //Debug.Log(rotD);
                
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



            if (hitTextureCooldown > Time.time)
            {
                HitTexture.SetActive(true);
            }
            else
            {
                HitTexture.SetActive(false);
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

    public void HitEffect()
    {
        hitTextureCooldown = Time.time + 0.1f;
    }
}
