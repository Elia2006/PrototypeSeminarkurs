using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public Transform cam;
    public Transform attackPoint;
    [SerializeField] GameObject Grenade;

    public float throwForce;
    public float throwUpwardForce;

    private GameObject HitTexture;
    private float hitTextureCooldown = 0;
    private float attackCooldown;
    // Start is called before the first frame update
    void Start()
    {
        HitTexture = GameObject.Find("HitTexture");
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetButtonDown("Fire1") && attackCooldown < Time.time)
            {
                ThrowGrenade();
                attackCooldown = Time.time + 2f;
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

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(Grenade, attackPoint.position, Quaternion.EulerAngles(cam.transform.forward));

        Rigidbody grenadeRb = grenade.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 50f)) 
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        grenadeRb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    public void HitEffect()
    {
        hitTextureCooldown = Time.time + 0.1f;
    }
}
