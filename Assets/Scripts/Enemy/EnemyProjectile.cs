using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float speed = 40;
    private HUD playerHUD;

    // Start is called before the first frame update
    void Start()
    {
        playerHUD = GameObject.Find("Player").GetComponent<HUD>();
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
