using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool collision = false;
    public bool teleport = false;
    public GameObject Map;
    public bool canMapOpen2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateTeleport()
    {
        if (collision) 
        {
            teleport = true;
            collision = false;
        }
    

    }

    private void OnTriggerEnter(Collider other)
    {
        collision = true;
        Map.GetComponent<Map>().canMapOpen = true;
    }
    private void OnTriggerExit(Collider other)
    {
        collision = false;
        Map.GetComponent<Map>().canMapOpen = false;
    }

}