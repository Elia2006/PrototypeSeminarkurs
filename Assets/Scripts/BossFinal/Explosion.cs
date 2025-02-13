using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float timer;
    private HUD hud;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + 1;
        hud = GameObject.Find("Player").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < Time.time)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            hud.TakeDamage(10, 1, transform, 0.1f);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
