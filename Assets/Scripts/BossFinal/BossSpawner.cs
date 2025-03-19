using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] Boss_new Boss;
    [SerializeField] FlamethrowerTarget flamethrowerTarget;
    [SerializeField] MaschineGunTarget maschineGunTarget;
    [SerializeField] GameObject bossHealth;
    [SerializeField] Animator doorAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Boss != null) 
        {
            Boss.StartBoss();
            flamethrowerTarget.enabled = true;
            maschineGunTarget.enabled = true;
            bossHealth.SetActive(true);
            doorAnim.SetBool("IsOpen", false);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
