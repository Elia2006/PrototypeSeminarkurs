using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private Vector3 defPos;
    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = defPos + new Vector3(0, Mathf.Sin(Time.time * 2) * 0.1f, 0);
        transform.rotation *= Quaternion.Euler(0, Time.deltaTime * 100, 0);
    }
}
