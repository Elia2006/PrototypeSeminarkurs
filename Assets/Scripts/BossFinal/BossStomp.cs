using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStomp : MonoBehaviour
{
    public int lifeCicle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        yield return new WaitForSeconds(0.2f);

        if(lifeCicle >= 0)
        {
            GameObject nextCicle = Instantiate(gameObject, transform.position + transform.forward * 2, transform.rotation);
            nextCicle.GetComponent<BossStomp>().lifeCicle = lifeCicle - 1;
        }

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

        yield return null;
    }
}
