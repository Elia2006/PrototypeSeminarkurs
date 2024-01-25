using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform cam;
    public GameObject trail;
    private GameObject tempTrail;

    private float range = 10;
    Vector3 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

    void Shoot() 
    {
        RaycastHit hit;

        Physics.Raycast(cam.position, cam.forward, out hit, range);
        Enemy enemy = hit.transform.GetComponent<Enemy>();

        if (enemy != null)
        {
            hitPoint = hit.point;
            enemy.TakeDamage(10);
        }
        else 
        {
            hitPoint = transform.position + transform.forward * range;
        }

  
        GameObject tempTrail = Instantiate(trail, transform.position, Quaternion.identity);
        tempTrail.GetComponent<BulletTrailMovement>().endPos = hitPoint;

        
    }
}
