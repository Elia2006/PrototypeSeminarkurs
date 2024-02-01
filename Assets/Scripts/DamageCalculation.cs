using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculation : MonoBehaviour
{
    [SerializeField] public float playerDamage = 10;
    [SerializeField] public float playerHealth = 50;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.Find("Enemy");

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerTakeDamage(Enemy.GetComponent<Enemy>().EnemyMeleeDamage);
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log(playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
    
        
    
    
        
    

