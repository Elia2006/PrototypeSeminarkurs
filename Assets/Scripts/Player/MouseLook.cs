using System;
using System.Collections;
using System.Collections.Generic;
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
        if (!PauseMenu.isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Cursorlock");
            //gets Mouse Input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            //prevents Overrotating
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
            //Rotates Body around X and Cam around Y
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);

            //cam wigwag
            Vector3 diff = transform.position - lastFramePos;

            float speed = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y + diff.z * diff.z);

            if (Player.GetComponent<PlayerMovement>().onGround)
            {
                wigwag += Mathf.Sqrt(speed);
            }

            transform.localPosition = new Vector3(0, 0.5f + Mathf.Sin(wigwag * 0.5f) * 0.07f, 0);

            lastFramePos = transform.position;
        }
        else if(PauseMenu.isPaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Debug.Log("open");
        }
    }
}
