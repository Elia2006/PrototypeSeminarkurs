using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualRotation : MonoBehaviour
{
    public SpiderAnimation frontRight;
    public SpiderAnimation frontLeft;
    public SpiderAnimation backRight;
    public SpiderAnimation backLeft;

    private Vector3 normalFrontRight;
    private Vector3 normalFrontLeft;
    private Vector3 normalBackRight;
    private Vector3 normalBackLeft;

    private float distanceFrontRight;
    private float distanceFrontLeft;
    private float distanceBackRight;
    private float distanceBackLeft;


    private Quaternion lerpAverage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        MoveLegs();
    }

    private void MoveLegs()
    {
        if(frontRight.distance + frontLeft.distance > 5)
        {
            if(frontRight.distance > frontLeft.distance)
            {
                frontRight.SetNewPos();
            }else
            {
                frontLeft.SetNewPos();
            }
        }
        if(backRight.distance + backLeft.distance > 5)
        {
            if(backRight.distance > backLeft.distance)
            {
                backRight.SetNewPos();
            }else
            {
                backLeft.SetNewPos();
            }
        }
        if(frontLeft.distance + backLeft.distance > 5)
        {
            if(frontLeft.distance > backLeft.distance)
            {
                frontLeft.SetNewPos();
            }else
            {
                backLeft.SetNewPos();
            }
        }
        if(backRight.distance + frontRight.distance > 5)
        {
            if(backRight.distance > frontRight.distance)
            {
                backRight.SetNewPos();
            }else
            {
                frontRight.SetNewPos();
            }
        }
    }

    private void Rotate()
    {
        RaycastHit hit;

        Physics.Raycast(frontRight.newPos + Vector3.up, Vector3.down, out hit, LayerMask.NameToLayer("Ground"), 10);
        normalFrontRight = hit.normal;

        Physics.Raycast(frontLeft.newPos + Vector3.up, Vector3.down, out hit, LayerMask.NameToLayer("Ground"), 10);
        normalFrontLeft = hit.normal;

        Physics.Raycast(backRight.newPos + Vector3.up, Vector3.down, out hit, LayerMask.NameToLayer("Ground"));
        normalBackRight = hit.normal;

        Physics.Raycast(backLeft.newPos + Vector3.up, Vector3.down, out hit, LayerMask.NameToLayer("Ground"));
        normalBackLeft = hit.normal;

        Vector3 average = (normalFrontRight + normalFrontLeft + normalBackRight + normalBackLeft) / 4;

        Quaternion quatAverage = Quaternion.FromToRotation(transform.up, average) * transform.rotation;

        transform.rotation = Quaternion.Euler(lerpAverage.eulerAngles.x, transform.rotation.eulerAngles.y, lerpAverage.eulerAngles.z);

        lerpAverage = Quaternion.Lerp(transform.rotation, quatAverage, 0.03f);
    }
}
