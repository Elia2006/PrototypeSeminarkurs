using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform Cam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject ImpactEffect;
    private GameObject HitTexture;
    [SerializeField] GameObject Line;
    [SerializeField] Transform GunEnd;
    private float hitTextureCooldown = 0;
    private float attackCooldown;

    // Start is called before the first frame update
    void Awake()
    {
        HitTexture = GameObject.Find("HitTexture");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && attackCooldown < Time.time)
        {
            Shoot();
            attackCooldown = Time.time + 0.7f;
        }
        
        if(hitTextureCooldown > Time.time)
        {
            HitTexture.SetActive(true);
        }else{
            HitTexture.SetActive(false);
        }
    }

    void Shoot() 
    {
        
        GameObject LineRenderer = Instantiate(Line, GunEnd.position, Quaternion.identity, Cam);

        RaycastHit hit;
        

        if(Physics.Raycast(Cam.position, Cam.forward, out hit))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(10);
                hitTextureCooldown = Time.time + 0.1f;
            }
            LineRenderer.transform.LookAt(hit.point);
        }else
        {
            LineRenderer.transform.LookAt(Cam.position + Cam.forward * 50);
        }
        Destroy(LineRenderer, 0.1f);


        muzzleFlash.Play();
        Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        

        /*
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
        

        Instantiate(Projectile, GunEnd.position, GunEnd.rotation);

        muzzleFlash.Play();
        */
    }
}
