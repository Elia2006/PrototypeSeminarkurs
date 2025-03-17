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
    public bool isAudio = false;

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

                GameObject gFireGround = Instantiate(fireGround, hit.point, Quaternion.identity);
                if(isAudio)
                {
                    gFireGround.GetComponent<AudioSource>().enabled = true;
                }
                
                alive = false;

            }
        }
    }
}
