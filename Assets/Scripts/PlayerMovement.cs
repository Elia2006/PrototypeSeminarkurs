using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 10;
    public float jumpHeight = 3;

    private float gravity = -9.81f * 2;
    private Vector3 velocity;

    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, groundMask);

        if (isGrounded) 
        {
            velocity.y = 0;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;

        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        }

        //gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
