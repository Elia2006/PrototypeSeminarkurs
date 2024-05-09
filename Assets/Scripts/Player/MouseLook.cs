using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1000;
    public float xRotation = 0;

    public Transform playerBody;
    public GameObject Player;
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
        }

    }
}
