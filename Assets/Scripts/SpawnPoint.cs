using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool collision = false;
    public bool teleport = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && collision)
        {
            teleport = true;
            collision = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collision = true;
    }
    private void OnTriggerExit(Collider other)
    {
        collision = false;
    }

}