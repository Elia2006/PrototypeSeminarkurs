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

    //Animation
    [SerializeField] Animator animShoot;
    [SerializeField] Animator animAim;


    //Audio
    private AudioSource shoot;

    void Awake()
    {
        position = transform.localPosition;
        shoot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused)
        {
            if (Input.GetButton("Fire1") && attackCooldown < Time.time)
            {
                shoot.Play();
                Shoot();
                attackCooldown = Time.time + 0.1f;
                
            }

            if(Input.GetButton("Fire1"))
            {
                animShoot.SetBool("IsShooting", true);
            }else
            {
                animShoot.SetBool("IsShooting", false);
            }

            if(Input.GetMouseButton(1))
            {
                animAim.SetBool("IsAiming", true);
                //acuracy = 1;

                Player.GetComponent<PlayerMovement>().isAiming = true;
            }else
            {
                animAim.SetBool("IsAiming", false);
                //acuracy = 10;
                Player.GetComponent<PlayerMovement>().isAiming = false;
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


        if(hit.point.sqrMagnitude > .01f)
        {
            GunEnd.LookAt(hit.point);
        }
        else
        {
            GunEnd.LookAt(Cam.position + Cam.forward * 50);
        }
        GunEnd.Rotate(-cappedAcuracy * 15, Random.Range(cappedAcuracy * 8, -cappedAcuracy * 8), 0);        

        GameObject proj = Instantiate(Projectile, GunEnd.position, GunEnd.rotation);
        proj.GetComponent<Projectile>().playerVelocity = Player.GetComponent<PlayerMovement>().direction;

        muzzleFlash.Play();
        
    }
}
