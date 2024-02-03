using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Projectile;
    public Transform Cam;
    private Transform BulletSpawnPoint;


    private float range = 10;

    // Start is called before the first frame update
    void Start()
    {
        BulletSpawnPoint = gameObject.transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Cam.position, Cam.forward, out hit))
        {
            BulletSpawnPoint.LookAt(hit.point);
        }else
        {
            transform.localRotation = Quaternion.identity;
        }

        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

    void Shoot() 
    {
        Instantiate(Projectile, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
    }
}
