using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMove6 : MonoBehaviour
{
    private int moveCicle;
    [SerializeField] Transform[] Legs;
    [SerializeField] Transform[] LegDefaultPos;
    private Vector3 lastPos;
    private Transform currentLeg;
    [SerializeField] LayerMask groundLayer;

    //Settings
    [SerializeField] float maxDistance;
    [SerializeField] float stepDistance;
    [SerializeField] float legLerp;
    [SerializeField] float legCheckHeight;

    Vector3 temp1;
    Vector3 temp2;

    // Start is called before the first frame update
    void Start()
    {
        currentLeg = Legs[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCicle >= Legs.Length)
        {
            moveCicle = 0;
        }

        float distance = Vector3.Distance(Legs[moveCicle].position, LegDefaultPos[moveCicle].position);


        if(distance > maxDistance && currentLeg.GetComponent<SpiderAnimation>().lerp >= legLerp)
        {
            RaycastHit hit;
            Vector3 direction = (transform.position - lastPos).normalized * stepDistance;
            Physics.Raycast(LegDefaultPos[moveCicle].transform.position + direction + transform.up * legCheckHeight, -transform.up, out hit, Mathf.Infinity, groundLayer);
            
            temp1 = LegDefaultPos[moveCicle].transform.position + direction + transform.up * 5;
            temp2 = hit.point;
            

            if(hit.point != Vector3.zero)
            {
                Legs[moveCicle].GetComponent<SpiderAnimation>().SetNewPos(hit.point);
                currentLeg = Legs[moveCicle];
            }
            
            moveCicle += 1;
        }
        lastPos = transform.position;
        Debug.DrawLine(temp1, temp2);
    }
}
