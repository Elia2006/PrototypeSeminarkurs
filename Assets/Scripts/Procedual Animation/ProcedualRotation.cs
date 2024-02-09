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

    private Quaternion lerpAverage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

        Debug.DrawLine(transform.position, transform.position + average);

        transform.rotation = Quaternion.Euler(lerpAverage.eulerAngles.x, transform.rotation.eulerAngles.y, lerpAverage.eulerAngles.z);

        lerpAverage = Quaternion.Lerp(transform.rotation, quatAverage, 0.03f);
    }
}
