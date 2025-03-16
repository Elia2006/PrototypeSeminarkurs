using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_new : MonoBehaviour
{
    private Vector3 Player;
    private Vector3 finalTarget;
    Rigidbody myRigidbody;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject Warning;
    private GameObject warning;
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myRigidbody.isKinematic = false;

        Player = GameObject.Find("Player").transform.position;

        float random = 13;

        RaycastHit hit;
        Physics.Raycast(Player + new Vector3(Random.Range(-random, random), 0, Random.Range(-random, random)), 
            Vector3.down, out hit, Mathf.Infinity, groundLayer);
        finalTarget = hit.point;

        warning = Instantiate(Warning, hit.point + Vector3.up * 0.1f, Quaternion.Euler(90, 0, 0));

    }


    // Update is called once per frame
    void Update()
    {

        /*if(tempTargetReached)
        {
            
        }else
        {   
            var lookRotation = Quaternion.LookRotation(tempTarget - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
        }

        if(!tempTargetReached && Vector3.Distance(tempTarget, transform.position) < 3)
        {
            tempTargetReached = true;
        }
        

        transform.position += transform.forward * Time.deltaTime * 20;*/

        var lookRotation = Quaternion.LookRotation(finalTarget - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.03f);

        myRigidbody.velocity = transform.forward * 10;
        //transform.Rotate(new Vector3(Mathf.Cos(Time.time), 0, 0));
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Ground") || collider.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(warning);
            Destroy(gameObject);
        }
    }
}
