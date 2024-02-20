using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 10;
    public float jumpHeight = 3;

    private float gravity = -9.81f * 3;
    private Vector3 velocity;

    private float groundDistance;
    private bool onGround;
    public Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        onGround = Physics.CheckSphere(groundCheck.position, 0.5f, groundMask);

        RaycastHit hit;
        Physics.Raycast(groundCheck.position, Vector3.down, out hit, Mathf.Infinity, groundMask);

        if(onGround && velocity.y < 0)
        {
            velocity.y = -2;
        }


        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && (hit.distance < 1 || onGround)) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        }

        //gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
