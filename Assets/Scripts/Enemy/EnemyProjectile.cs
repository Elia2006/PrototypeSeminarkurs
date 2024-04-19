using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float speed = 40;
    private HUD playerHUD;
    private Transform Player;

    private float spread = 5;

    // Start is called before the first frame update
    void Awake()
    {
        playerHUD = GameObject.Find("Player").GetComponent<HUD>();
        Player = GameObject.Find("Player").GetComponent<Transform>();
        var playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        var rotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);
        transform.localRotation = Quaternion.Euler(rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
        transform.rotation *= Quaternion.Euler(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }else if(other.transform.CompareTag("Player"))
        {
            playerHUD.TakeDamage(5);
            Destroy(gameObject);
        }
    }
}
