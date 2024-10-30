using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAnimation : MonoBehaviour
{
    [SerializeField] Transform defaultPos;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = defaultPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        lastPos = Vector3.Lerp(lastPos, defaultPos.position, 0.05f);

        transform.position = lastPos;
    }
}
