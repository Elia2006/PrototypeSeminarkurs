using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public bool phase2 = false;
    public int grenadeDamage;

    public float delay = 3f;

    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && hasExploded == false)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Gegner getroffen");
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            
            collision.gameObject.GetComponent<Enemy>().TakeDamage(grenadeDamage);
            if (phase2)
            {
                areaOfEffectDamage();
            }

        } if (collision.gameObject.CompareTag("Boss"))
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);

            collision.gameObject.GetComponent<Boss>().BossTakeDamage(grenadeDamage / 2);
            if (phase2)
            {
                areaOfEffectDamage();
            }
        } 
    }

    private void areaOfEffectDamage()
    {
        //leave behind aoe
    }
}
