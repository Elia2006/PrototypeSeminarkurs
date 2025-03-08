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
    public GameObject hitTexture;
    public Image hitTextureImage;
    [SerializeField] GameObject hitParticle;
    [SerializeField] int damage;
    Vector3 lastPos;

    public Vector3 playerVelocity;

    //Layers
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;




    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lastPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        
        distanceTravelled = speed * (Time.time - startTime);
        transform.position += (transform.forward * speed + playerVelocity) * Time.deltaTime;
        
        if(distanceTravelled >= range)
        {
            Destroy(gameObject);
        }

        lastPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {

        RaycastHit hit;

        Physics.Raycast(lastPos, transform.forward, out hit, Mathf.Infinity, groundLayer | enemyLayer);
     

        if(other.gameObject.CompareTag("Enemy") && other.gameObject.TryGetComponent<CollisionScript>(out CollisionScript collisionScript)){

            Debug.Log("lol");

            collisionScript.TakeDamage(damage);
            
            if(collisionScript.GetIsWeakpoint())
            {
                hitTextureImage.color =  Color.red;
            }else
            {
                hitTextureImage.color = Color.white;
            }
            hitTexture.GetComponent<HitTextureS>().Hit();
            Destroy(gameObject);
        }else if(other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
        
        
    }
}
