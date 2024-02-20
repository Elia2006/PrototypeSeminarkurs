using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            Debug.DrawLine(transform.position, hit.point);
        }else
        {
            Debug.Log("hole");
        }


    }
}
