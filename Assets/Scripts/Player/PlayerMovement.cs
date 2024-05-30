using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public bool locked = false;

    private float speed;
    private Vector3 move;
    public float jumpHeight = 3;

    private float gravity = -9.81f * 3;
    private Vector3 velocity;

    private float groundDistance;
    public bool onGround;
    public Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    public Vector3 direction;
    private Vector3 lastPos;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Application.targetFrameRate = 120;
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

        //Sprint
        if(y > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8;
        }else
        {
            speed = 3;
        }

        if(onGround)
        {
            move = transform.right * x + transform.forward * y;
        }else
        {
            //aircontroll
            move = Vector3.Lerp(move, transform.right * x + transform.forward * y, 0.05f);
        }
        if(!locked) {

            controller.Move(move.normalized * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && (hit.distance < 1 || onGround))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

            }
            
            //gravity
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            calculateDirection();
        }
        

    }

    private void calculateDirection()
    {
        direction = transform.position - lastPos;
        lastPos = transform.position;
    }

    public void LockTheGame()
    {
        if (GameObject.Find("InventoryManager").GetComponent<InventoryManager>().invactive == false && GameObject.Find("Map").GetComponent<Map>().mapOpen==false)
        {
            locked = false;
            Debug.Log(locked + "playermovement");

        }
    }
}
