using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSystemPoint : MonoBehaviour
{
    public bool interact = false;
    private Transform Cam;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("PlayerCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Physics.Raycast(Cam.position, Cam.forward, out hit, 3);

        if(hit.collider == gameObject.GetComponent<Collider>() && Input.GetKeyDown(KeyCode.E))
        {
            interact = true;
        }
        else
        {
            interact = false;
        }
    }
}
