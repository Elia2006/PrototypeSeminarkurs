using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1000;
    public float xRotation = 0;

    public Transform playerBody;
    public GameObject Player;
    
    //camwigwag
    private float wigwag; 
    private Vector3 lastFramePos;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (!Player.GetComponent<PlayerMovement>().locked)
        {
            //gets Mouse Input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            
            //prevents Overrotating
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
            //Rotates Body around X and Cam around Y
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);

            camWigWag();


        }
    }

    private void camWigWag()
    {
        PlayerMovement playerMov = Player.GetComponent<PlayerMovement>();
        if(playerMov.dodgeTimer > Time.time)
        {
            float percentOfDodge = (playerMov.dodgeTimer - Time.time) / 0.1f;
            if(percentOfDodge < 0.5f)
            {
                transform.localPosition = new Vector3(0, Mathf.Lerp(0.6f, 0, percentOfDodge * 2), 0);
                Debug.Log(percentOfDodge);
            }else
            {
                transform.localPosition = new Vector3(0, Mathf.Lerp(0, 0.6f, (percentOfDodge - 0.5f) * 2), 0);
            }
            
        }
        else if(playerMov.move != new Vector3())
        {
            Vector3 diff = transform.position - lastFramePos;

            float speed = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y + diff.z * diff.z);

            if(playerMov.onGround)
            {
                wigwag += Mathf.Sqrt(speed);
            }

            transform.localPosition = new Vector3(0, 0.6f + Mathf.Sin(wigwag * 0.5f) * 0.07f, 0);

            lastFramePos = transform.position;
        }

    }
}
