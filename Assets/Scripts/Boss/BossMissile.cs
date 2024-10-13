using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : MonoBehaviour
{
    GameObject Player;
    public float speed = 10;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Spieler getroffen");
            DestroyMissile();
            //collision.gameObject.GetComponent<Player>().TakeDamage(grenadeDamage);

        }
    }

    private void DestroyMissile()
    {
        Destroy(gameObject);
    }
}
