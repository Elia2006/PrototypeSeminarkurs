using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Projectile : MonoBehaviour
{
    private HUD hud;
    private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.Find("Player").GetComponent<HUD>();
        lifeTime = Time.time + 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * 50);

        if(lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            hud.TakeDamage(5, 2, transform, 0.05f);
            Destroy(gameObject);
        }
    }
}
