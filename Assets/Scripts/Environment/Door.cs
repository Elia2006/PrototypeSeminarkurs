using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] InteractSystemPoint InteractSys;

    private float lerp = 0;
    private int lerpDirection = -1;
    private Vector3 openPos;
    private Vector3 closedPos;

    // Start is called before the first frame update
    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + Quaternion.AngleAxis(90, Vector3.up) * transform.forward * 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(InteractSys.interact == true)
        {
            lerpDirection *= -1;
        }

        lerp += Time.deltaTime * lerpDirection;

        if(lerp < 0)
        {
            lerp = 0;
        }
        else if(lerp > 1)
        {
            lerp = 1;
        }

        transform.position = Vector3.Lerp(closedPos, openPos, lerp);
        
    }
}
