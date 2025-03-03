using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject bossHealth;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Boss.SetActive(true);
            bossHealth.SetActive(true);
        }
    }
}
