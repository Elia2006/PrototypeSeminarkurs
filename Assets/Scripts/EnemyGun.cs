using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject Projectile;
    private Transform BulletSpawnPoint;
    public GameObject Player;
    Quaternion rotation;
    Vector3 pointTowardsPlayer;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        BulletSpawnPoint = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Wenn der Gegner nicht immer direkt weiß, wo der Spieler ist
        //Vector3 pointTowardsPlayer = Player.transform.position - transform.position;
        //rotation = Quaternion.LookRotation(pointTowardsPlayer, Vector3.up);
        LookForPlayer();
    }


    void LookForPlayer()
    {
        
        
        RaycastHit hit;
        if (Physics.Raycast(BulletSpawnPoint.position, transform.forward, out hit))
        {
            if (hit.collider != null && hit.transform.CompareTag("Player"))
            {

                Shoot();
            }
        }
        
    }
    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(Projectile, BulletSpawnPoint.position, transform.rotation);
            
        }
        
    }
}
