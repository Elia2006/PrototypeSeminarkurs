using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1000;
    private float xRotation = 0;

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
        //gets Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        
        //prevents Overrotating
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
    if (!Player.GetComponent<PlayerMovement>().locked)
        {
            //Rotates Body around X and Cam around Y
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        

    }
}
