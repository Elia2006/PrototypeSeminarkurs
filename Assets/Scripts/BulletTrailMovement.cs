using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrailMovement : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 endPos = new Vector3(0, 0, 0);
    private float move = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move += 0.1f;

        transform.position = Vector3.Lerp(startPos, endPos, move);
    }
}
