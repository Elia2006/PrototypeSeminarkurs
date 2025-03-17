using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualArmMovement : MonoBehaviour
{
    [SerializeField] Transform legDef;
    [SerializeField] Transform leg;
    [SerializeField] Transform def;
    [SerializeField] Transform aimTarget;
    [SerializeField] Scavanger scav;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scav != null && scav.continueCharge)
        {
            transform.position = aimTarget.position;
        }else
        {
            transform.position = def.position + (legDef.position - leg.position) * 0.8f;
        }
    }
}
