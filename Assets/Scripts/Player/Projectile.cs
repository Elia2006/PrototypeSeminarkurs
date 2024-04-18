using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public float range;

    private float startTime;
    private float distanceTravelled;
    private Gun gun;

    
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        gun = GameObject.Find("Gun").GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lastPosition = transform.position;

        distanceTravelled = speed * (Time.time - startTime);
        transform.position += transform.forward * speed * Time.deltaTime;
        
        if(distanceTravelled >= range)
        {
            Destroy(gameObject);
        }

        if(Physics.Linecast(transform.position, lastPosition, out hit) && !hit.transform.CompareTag("Player"))
        {
            OnTriggerEnter(hit.transform.GetComponent<Collider>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if(other.CompareTag("Enemy")){
            other.GetComponent<Enemy>().TakeDamage(10);
            gun.HitEffect();
        }
    }
}
