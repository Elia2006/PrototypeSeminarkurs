using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour
{
    [SerializeField] float legMoveSpeed = 8;
    private Vector3 currentPos;
    private Vector3 oldPos;
    public Vector3 newPos;
    public float lerp = 1;

    //Audio
    private AudioSource stepSound;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        stepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        LerpToNewPos();

        transform.position = currentPos;
    }

    public void SetNewPos(Vector3 newPosition)
    {
        stepSound.Play();
        oldPos = transform.position;
        newPos = newPosition;
        lerp = 0;
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