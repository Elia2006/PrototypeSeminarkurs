using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour
{
    public Transform Spider;
    public Transform targetPos;
    [SerializeField] LayerMask groundLayer;

    private float legMoveDistance = 2;
    private Vector3 currentPos;
    private Vector3 oldPos;
    public Vector3 newPos;
    public float lerp = 1;

    private Vector3 oldSpiderPos;
    private Vector3 spiderDirection;

    public SpiderAnimation diagonalLeg1;
    public SpiderAnimation diagonalLeg2;
    public SpiderAnimation oppositeLeg;


    // Start is called before the first frame update
    void Start()
    {
        currentPos = targetPos.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(currentPos, targetPos.position) > legMoveDistance && !(lerp < 1) &&
            !(diagonalLeg1.lerp < 1) && !(diagonalLeg2.lerp < 1))
        {
            SetNewPos();
            oppositeLeg.SetNewPos();

        }

        LerpToNewPos();

        transform.position = currentPos;

        oldSpiderPos = Spider.position;
    }

    public void SetNewPos()
    {
        oldPos = currentPos;

        lerp = 0;

        spiderDirection = (Spider.position - oldSpiderPos).normalized;

        RaycastHit hit;
        Vector3 newRawPos = targetPos.position + spiderDirection * (legMoveDistance - 1);
        Vector3 rayOrigin = newRawPos + Vector3.up;

        if(Physics.Raycast(rayOrigin, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            newPos =  hit.point;
            //Debug.DrawLine(rayOrigin, hit.point, Color.red, 60);
        }
    }

    private void LerpToNewPos()
    {
        if(lerp < 1)
        {
            lerp += 0.01f;
            currentPos = Vector3.Lerp(oldPos, newPos, lerp);
            currentPos.y += Mathf.Sin(lerp * Mathf.PI) * 0.5f;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPos, 0.5f);
    }
}