using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField] LayerMask enemyLayer;

    //Ammo
    private int loadedAmmo = 0;
    private TextMeshProUGUI loadedAmmoText; 
    private readonly int maxLoadedAmmo = 20;

    public int availableAmmo = 30;
    private TextMeshProUGUI availableAmmoText; 

    //Animation
    [SerializeField] Animator animShoot;
    [SerializeField] Animator animAim;


    //Audio
    [SerializeField] AudioSource shoot;
    [SerializeField] AudioSource reload;

    //hitTexture
    private GameObject hitTexture;
    private Image hitTextureImage;

    void Awake()
    {
        position = transform.localPosition;
        shoot = GetComponent<AudioSource>();

        loadedAmmoText = GameObject.Find("LoadedAmmo").GetComponent<TextMeshProUGUI>();
        availableAmmoText = GameObject.Find("AvailableAmmo").GetComponent<TextMeshProUGUI>();

        hitTexture = GameObject.Find("HitTexture");
        hitTextureImage = hitTexture.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused)
        {
            if (Input.GetButton("Fire1") && attackCooldown < Time.time && loadedAmmo > 0)
            {
                shoot.Play();
                Shoot();
                attackCooldown = Time.time + 0.1f;
                
            }

            if(Input.GetButton("Fire1") && loadedAmmo > 0)
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

        Ammo();
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
        loadedAmmo--;

        RaycastHit hit;

        Physics.Raycast(Cam.position, Cam.forward, out hit, Mathf.Infinity, enemyLayer);


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
        proj.GetComponent<Projectile>().hitTexture = hitTexture;
        proj.GetComponent<Projectile>().hitTextureImage = hitTextureImage;

        muzzleFlash.Play();
        
    }

    private void Ammo()
    {
        if(Input.GetKeyDown(KeyCode.R) && loadedAmmo < maxLoadedAmmo && availableAmmo > 0)
        {  
            attackCooldown = Time.time + 0.5f; 
            reload.Play();

            availableAmmo -= maxLoadedAmmo - loadedAmmo;
            loadedAmmo = maxLoadedAmmo;

            if(availableAmmo < 0)
            {
                loadedAmmo += availableAmmo;
                availableAmmo = 0;
            }
        }

        loadedAmmoText.text = loadedAmmo +  "";
        availableAmmoText.text = availableAmmo + "";
    }
}
