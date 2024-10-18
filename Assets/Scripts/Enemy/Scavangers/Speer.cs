using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speer : MonoBehaviour
{
    private GameObject Player;
    public float velocityY = 0;
    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        lastPos = transform.position;
        transform.LookAt(Player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        velocityY -= 1;
        transform.position += (transform.forward * 30 + new Vector3(0, velocityY, 0)) * Time.deltaTime;

        RaycastHit hit;
        if(Physics.Linecast(transform.position, lastPos, out hit))
        {
            OnTriggerEnter(hit.transform.GetComponent<Collider>());
        }
        lastPos = transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }else if(other.transform.CompareTag("Player"))
        {
            Player.GetComponent<HUD>().TakeDamage(5);
            Destroy(gameObject);
        }
    }
}
