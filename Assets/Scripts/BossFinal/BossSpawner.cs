using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] Boss_new Boss;
    [SerializeField] GameObject bossHealth;
    [SerializeField] Animator doorAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Boss.enabled = true;
            bossHealth.SetActive(true);
            doorAnim.SetBool("IsOpen", false);
        }
    }
}
