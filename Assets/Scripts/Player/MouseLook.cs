using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1000;
    public float xRotation = 0;

    public Transform playerBody;
    public GameObject Player;
    public HUD hud;

    public Map map;
    
    //camwigwag
    private float wigwag; 
    private Vector3 lastFramePos;

    //Audio
    private AudioSource step;
    Boolean audioReset;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        step = GetComponent<AudioSource>();
        hud = Player.GetComponent<HUD>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (!PauseMenu.isPaused && !map.mapOpen && hud.playerHealth>0)
        {
            Cursor.lockState = CursorLockMode.Locked;

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


        }else if(PauseMenu.isPaused || map.mapOpen || hud.playerHealth<=0)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }


    private void camWigWag()
    {
        PlayerMovement playerMov = Player.GetComponent<PlayerMovement>();

        if(playerMov.move != new Vector3())
        {
            Vector3 diff = transform.position - lastFramePos;

            float speed = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y + diff.z * diff.z);

            if(playerMov.onGround)
            {
                wigwag += Mathf.Sqrt(speed);
            }

            transform.localPosition = new Vector3(0, 0.6f + Mathf.Sin(wigwag * 0.5f) * 0.07f, 0);

            if(Mathf.Sin(wigwag * 0.5f) > 0.9f && audioReset)
            {
                step.Play();
                audioReset = false;
            }
            if(Mathf.Sin(wigwag * 0.5f) < 0.9f)
            {
                audioReset = true;
            }


            lastFramePos = transform.position;
        }

    }
}
