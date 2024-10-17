using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 20;
    public float range;

    private HUD playerHUD;
    private Transform Player;

    



   

    // Start is called before the first frame update
    void Awake()
    {
        playerHUD = GameObject.Find("Player").GetComponent<HUD>();
        Player = GameObject.Find("Player").GetComponent<Transform>();
        var playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        var rotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
 
    

    

    // Start is called before the first frame update
    


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(10);
            
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Spieler nimmt Schaden");
        }
    }
}