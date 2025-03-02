using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Outpost : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SawPlayer(Vector3 pos)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.goingToLastPos = true;
            enemy.GetComponent<NavMeshAgent>().destination = pos;
        }
    }
}
