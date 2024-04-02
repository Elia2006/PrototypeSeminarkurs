using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactScript : MonoBehaviour
{
    [SerializeField] int damage;
    private float lifetime = 0;
    private bool enabled = true;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(lifetime < 0.1f){
            transform.localScale = new Vector3(lifetime, lifetime, lifetime) * 15;
        }

        lifetime += Time.deltaTime;
        if(lifetime > 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other == Player.GetComponent<Collider>() && enabled)
        {
            Player.GetComponent<HUD>().TakeDamage(damage);
            enabled = false;
        }
    }
}
