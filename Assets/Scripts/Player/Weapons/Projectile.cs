using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    private float speed = 100;
    private float range = 40;

    private float startTime;
    private float distanceTravelled;
    private GameObject hitTexture;
    [SerializeField] GameObject hitParticle;
    [SerializeField] int damage;
    Vector3 lastPos;

    



    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lastPos = transform.position;
        hitTexture = GameObject.Find("HitTexture");
    }


    // Update is called once per frame
    void Update()
    {
        
        distanceTravelled = speed * (Time.time - startTime);
        transform.position += transform.forward * speed * Time.deltaTime;
        
        if(distanceTravelled >= range)
        {
            Destroy(gameObject);
        }

        /*
        if(Physics.Linecast(transform.position, lastPos, out hit) && !hit.transform.CompareTag("Player"))
        {
            OnTriggerEnter(hit.transform.GetComponent<Collider>());
        }*/
        lastPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {

        RaycastHit hit;
        Physics.Raycast(lastPos, transform.forward, out hit);
        
                            

        if(other.CompareTag("Enemy")){
            Debug.Log(other.transform);

            
            other.GetComponent<CollisionScript>().TakeDamage(damage);

            
            if(other.GetComponent<CollisionScript>().GetIsWeakpoint())
            {
                hitTexture.GetComponent<Image>().color =  Color.red;
            }else
            {
                hitTexture.GetComponent<Image>().color = Color.white;
            }
            hitTexture.GetComponent<HitTextureS>().Hit();
            Destroy(gameObject);
            Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
        }else if(other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Instantiate(hitParticle, lastPos, Quaternion.LookRotation(hit.normal));
        }
        /*else if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().BossTakeDamage(20);
            gun.HitEffect();
        }*/
        
        
    }
}
