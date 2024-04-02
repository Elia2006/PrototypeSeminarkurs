using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector3 pos0 = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        pos0 += new Vector3(0, 0, 0.2f);
        lineRenderer.SetPosition(0, pos0);
    }
}
