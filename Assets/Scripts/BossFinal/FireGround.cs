using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGround : MonoBehaviour
{
    private float lifeTime;
    private HUD playerHUD;

    private float damageCooldown;
    private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = Time.time;

        playerHUD = GameObject.Find("Player").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alive)
        {
            if(lifeTime < Time.time - 60)
            {
                alive = false;
            }
        }

        if(lifeTime < Time.time - 62)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(alive)
        {
            if(collider.transform.CompareTag("Player") && damageCooldown < Time.time)
            {
                playerHUD.TakeDamage(2, 3, transform.position, 0);

                damageCooldown = Time.time + 0.5f;
            }
        }
    }
}
