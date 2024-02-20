using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToGround : MonoBehaviour
{
    public Transform Spider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3(transform.position.x, Spider.position.y, transform.position.z);

        Physics.Raycast(rayOrigin, Vector3.down, out hit, LayerMask.NameToLayer("Ground"));

        transform.position = hit.point;
    }
}
