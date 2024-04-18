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
    private float legMoveSpeed = 3;
    private Vector3 currentPos;
    private Vector3 oldPos;
    public Vector3 newPos;
    public float lerp = 1;

    private Vector3 oldSpiderPos;
    private Vector3 spiderDirection;

    public SpiderAnimation diagonalLeg1;
    public SpiderAnimation diagonalLeg2;
    public SpiderAnimation oppositeLeg;
    public float distance;


    // Start is called before the first frame update
    void Start()
    {
        currentPos = targetPos.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        spiderDirection = (Spider.position - oldSpiderPos).normalized;


        distance = Vector3.Distance(newPos, targetPos.position + spiderDirection * 1.5f);

        LerpToNewPos();

        transform.position = currentPos;

        oldSpiderPos = Spider.position;
    }

    public void SetNewPos()
    {
        oldPos = currentPos;

        lerp = 0;

        RaycastHit hit;
        Vector3 newRawPos = targetPos.position + spiderDirection * 1.5f;
        Vector3 rayOrigin = newRawPos + Vector3.up;

        if(Physics.Raycast(rayOrigin, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            newPos =  hit.point;
        }
    }

    private void LerpToNewPos()
    {
        if(lerp < 1)
        {
            lerp += Time.deltaTime * legMoveSpeed;
            currentPos = Vector3.Lerp(oldPos, newPos, lerp);
            currentPos.y += Mathf.Sin(lerp * Mathf.PI) * 0.5f;
        }
    }

    void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPos, 0.5f);*/
    }
}