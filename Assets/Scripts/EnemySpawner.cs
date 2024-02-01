using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private Transform EnemySpawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("SpawnEnemy", 1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    { 
        Instantiate(EnemyPrefab, EnemySpawnPoint);
    }
}
