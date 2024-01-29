using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public float range;

    private float startTime;
    private float distanceTravelled;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
