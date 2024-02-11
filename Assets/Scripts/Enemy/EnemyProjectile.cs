using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 5);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
