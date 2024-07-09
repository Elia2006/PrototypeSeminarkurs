using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LegMove6 : MonoBehaviour
{
    private int moveCicle;
    [SerializeField] Transform[] Legs;
    [SerializeField] Transform[] LegDefaultPos;
    private Vector3 lastPos;
    private Transform currentLeg;
    [SerializeField] LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        currentLeg = Legs[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCicle > 5)
        {
            moveCicle = 0;
        }

        

        float distance = Vector3.Distance(Legs[moveCicle].GetComponent<SpiderAnimation>().newPos, LegDefaultPos[moveCicle].transform.position);

        if(distance > 0.2f && currentLeg.GetComponent<SpiderAnimation>().lerp >= 0.5f)
        {
            RaycastHit hit;
            Vector3 direction = (transform.position - lastPos).normalized * 0.5f;
            Physics.Raycast(LegDefaultPos[moveCicle].transform.position + direction + transform.up, -transform.up, out hit, Mathf.Infinity, groundLayer);

            if(distance > Vector3.Distance(hit.point, LegDefaultPos[moveCicle].transform.position))
            {
                Legs[moveCicle].GetComponent<SpiderAnimation>().SetNewPos(hit.point);
                currentLeg = Legs[moveCicle];
            }
            
            moveCicle += 1;
        }
        lastPos = transform.position;

    }
}
