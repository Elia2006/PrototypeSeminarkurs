using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform Cam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject ImpactEffect;
    [SerializeField] Transform GunEnd;
    private float attackCooldown;
    public GameObject Player;

    private float acuracy = 0;
    private float cappedAcuracy;
    [SerializeField] float minAcuracy;
    [SerializeField] float maxAcuracy;
    [SerializeField] float acuracyAdd;
    private Vector3 position;
    [SerializeField] float weaponOffset;


    // Start is called before the first frame update
    void Awake()
    {
        position = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused)
        {
            if (Input.GetButton("Fire1") && attackCooldown < Time.time)
            {
                Shoot();
                attackCooldown = Time.time + 0.1f;



                transform.localPosition = position + new Vector3(Random.Range(-weaponOffset, weaponOffset), 
                Random.Range(-weaponOffset, weaponOffset), Random.Range(-weaponOffset, weaponOffset) - acuracy * 0.3f);

                
            }

            if(!Input.GetButton("Fire1"))
            {
                transform.localPosition = position;
            }
            
            Acuracy();

        }


    }

    private void Acuracy()
    {
        if(Input.GetButton("Fire1") && acuracy < maxAcuracy)
        {
            acuracy += acuracyAdd;
        }
        else if(acuracy > minAcuracy)
        {
            acuracy -= Time.deltaTime * 3;
        }else
        {
            acuracy = minAcuracy;
        }
        cappedAcuracy = acuracy - 0.5f;
        if(cappedAcuracy < 0)
        {
            cappedAcuracy = 0;
        }


    }

    void Shoot() 
    {
        RaycastHit hit;

        Physics.Raycast(Cam.position, Cam.forward, out hit);

        if(hit.transform != null && !hit.transform.CompareTag("Player"))
        {
            GunEnd.LookAt(hit.point);
        }
        else
        {
            GunEnd.LookAt(Cam.position + Cam.forward * 50);
        }
        GunEnd.Rotate(-cappedAcuracy * 15, Random.Range(cappedAcuracy * 8, -cappedAcuracy * 8), 0);        

        Instantiate(Projectile, GunEnd.position, GunEnd.rotation);

        muzzleFlash.Play();
        
    }
}
