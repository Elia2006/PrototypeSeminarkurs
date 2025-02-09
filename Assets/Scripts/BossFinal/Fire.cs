using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private float lifeTime;
    private HUD playerHUD;

    private float damageCooldown;
    private bool alive = true;
    [SerializeField] GameObject fireGround;
    [SerializeField] LayerMask groundLayer;

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
            transform.position += transform.forward * (Time.deltaTime * 20);

            if(lifeTime < Time.time - 2)
            {
                alive = false;
            }
        }


        if(lifeTime < Time.time - 4)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(alive)
        {
            if(collider.transform.CompareTag("Ground"))
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer);

                Instantiate(fireGround, hit.point, Quaternion.identity);
                alive = false;

            }else if(collider.transform.CompareTag("Player") && damageCooldown < Time.time)
            {
                playerHUD.TakeDamage(1, 3, transform, 0);

                damageCooldown = Time.time + 0.1f;
            }
        }
    }
}
