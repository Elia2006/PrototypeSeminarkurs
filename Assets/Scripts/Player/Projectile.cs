using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 100;
    private float range = 40;

    private float startTime;
    private float distanceTravelled;
    private Gun gun;
    [SerializeField] GameObject hitParticle;
    Vector3 lastPos;

    
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        lastPos = transform.position;
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
            other.GetComponent<Enemy>().TakeDamage(10);
            gun.HitEffect();
            Destroy(gameObject);
            Instantiate(hitParticle, lastPos, Quaternion.LookRotation(hit.normal));
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
