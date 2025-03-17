using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speer : MonoBehaviour
{
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        transform.LookAt(Player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * 15);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }else if(other.transform.CompareTag("Player"))
        {
            Player.GetComponent<HUD>().TakeDamage(5, 1, transform.position, 0.05f);
            Destroy(gameObject);
        }
    }
}
