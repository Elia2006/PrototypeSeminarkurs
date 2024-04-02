using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSystemBox : MonoBehaviour
{
    public bool interact = false;
    private bool collision = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && collision)
        {
            interact = true;
        }else
        {
            interact = false;
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
