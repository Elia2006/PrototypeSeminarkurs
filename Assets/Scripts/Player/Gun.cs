using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform Cam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject ImpactEffect;
    [SerializeField] GameObject HitTexture;
    private float hitTextureCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        HitTexture = GameObject.Find("HitTexture");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
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

        RaycastHit hit;

        if(Physics.Raycast(Cam.position, Cam.forward, out hit))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(10);
                hitTextureCooldown = Time.time + 0.1f;
            }
        }
        muzzleFlash.Play();
        Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
