using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualRotation : MonoBehaviour
{
    [SerializeField] SpiderAnimation[] Legs;
    private Vector3 average;


    private Quaternion lerpAverage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        RaycastHit hit;

        average = new Vector3(0, 0, 0);
        foreach(SpiderAnimation leg in Legs)
        {
            Physics.Raycast(leg.newPos + transform.up, -transform.up, out hit, LayerMask.NameToLayer("Ground"));
            average += hit.normal;
        }
        average /= Legs.Length;

        Quaternion quatAverage = Quaternion.FromToRotation(transform.up, average) * transform.rotation;

        transform.rotation = Quaternion.Euler(lerpAverage.eulerAngles.x, transform.rotation.eulerAngles.y, lerpAverage.eulerAngles.z);

        lerpAverage = Quaternion.Lerp(transform.rotation, quatAverage, 0.03f);
    }
}
