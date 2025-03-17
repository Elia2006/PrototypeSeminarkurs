using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform Cam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject ImpactEffect;
    [SerializeField] Transform GunEnd;
    private float attackCooldown;
    public GameObject Player;
    [SerializeField] LayerMask enemyLayer;

    //Aim
    private float acuracy = 0;

    //Animation
    [SerializeField] Animator animShoot;
    [SerializeField] Animator animAim;

    //Ammo
    private int loadedAmmo = 0;
    private TextMeshProUGUI loadedAmmoText; 
    private readonly int maxLoadedAmmo = 6;

    public int availableAmmo = 50;
    private TextMeshProUGUI availableAmmoText; 
    
    //Audio
    [SerializeField] AudioSource shoot;
    [SerializeField] AudioSource reload;

    //hitTexture
    private GameObject hitTexture;
    private Image hitTextureImage;

    void Start()
    {
        loadedAmmoText = GameObject.Find("LoadedAmmo").GetComponent<TextMeshProUGUI>();
        availableAmmoText = GameObject.Find("AvailableAmmo").GetComponent<TextMeshProUGUI>();

        hitTexture = GameObject.Find("HitTexture");
        hitTextureImage = hitTexture.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && attackCooldown < Time.time && loadedAmmo > 0)
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
            acuracy = 5;
            Player.GetComponent<PlayerMovement>().isAiming = false;
        }

        Ammo();
    }

    void Shoot() 
    {
        animShoot.SetTrigger("Shoot");
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

        Vector3 spread = new(Random.Range(acuracy, -acuracy), Random.Range(acuracy, -acuracy), 0);

        GameObject proj = Instantiate(Projectile, GunEnd.position, GunEnd.rotation * Quaternion.Euler(spread));
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
