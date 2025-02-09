using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 rotation;
    public float rotationSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.localRotation *= Quaternion.Euler();

        Vector3 finalRotation = rotation * rotationSpeed * Time.deltaTime;

        transform.Rotate(finalRotation.x, finalRotation.y, finalRotation.z);
    }
}
