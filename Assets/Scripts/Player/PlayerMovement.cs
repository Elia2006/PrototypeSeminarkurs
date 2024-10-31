using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public bool locked = false;

    private float speed;
    private float x;
    private float y;
    public Vector3 move;
    public float jumpHeight = 2;

    private float gravity = -9.81f * 3;
    private Vector3 velocity;

    private float groundDistance;
    public bool onGround;
    public Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    public Vector3 direction;
    private Vector3 lastPos;

    //Knockback
    private Vector3 KnockbackForce;

    private float speedReductionTimer;
    private float speedReduction;

    //Dodge
    public float dodgeTimer;
    private float dodgeCooldown;
    private Vector3 dodgeDirection;

    //sliding from steep surface
    private bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Application.targetFrameRate = 120;
        lastPos = transform.position;
    }

    void Update()
    {
        onGround = Physics.CheckSphere(groundCheck.position, 0.6f, groundMask);
        checkStandingSurface();

        RaycastHit hit;
        Physics.Raycast(groundCheck.position, Vector3.down, out hit, Mathf.Infinity, groundMask);

        if(onGround && velocity.y < 0)
        {
            velocity.y = -2;
        }


        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        

        Move();

        Dodge();

        Sprint();
        if(!locked && canMove) {

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

        //Knockback
        controller.Move(KnockbackForce);
        KnockbackForce = Vector3.Lerp(KnockbackForce, new Vector3(), 0.05f);

        

    }

    private void Dodge()
    {
        if(dodgeTimer > Time.time)
        {
            controller.Move(dodgeDirection * 30 * Time.deltaTime);
        }
        else if(Input.GetKeyDown(KeyCode.LeftAlt) && dodgeCooldown < Time.time)
        {
            dodgeTimer = 0.1f + Time.time;
            dodgeCooldown = 2 + Time.time;
            dodgeDirection = move;
        }
    }

    
    private void Sprint()
    {
        if(speedReductionTimer > Time.time)
        {
            speed = speedReduction;
        }
        else if(y > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8;
        }else
        {
            speed = 3;
        }
    }

    private void Move()
    {
        if(onGround)
        {
            move = transform.right * x + transform.forward * y;
        }else
        {
            //aircontroll
            move = Vector3.Lerp(move, transform.right * x + transform.forward * y, 0.05f);
        }
    }

    private void checkStandingSurface()
    {
        canMove = true;
        if(onGround){
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundMask);
            Debug.DrawLine(hit.point, hit.point + hit.normal);

            if(hit.normal.y < 0.75f)
            {
                canMove = false;
                controller.Move(new Vector3(hit.normal.x * 0.05f, 0, hit.normal.z * 0.05f));
            }
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

    public void Knockback(Transform enemy)
    {
        KnockbackForce = (transform.position - enemy.position).normalized * 0.2f;
    }

    public void ReduceSpeed(float speed, float time)
    {
        speedReduction = speed;
        speedReductionTimer = Time.time + time;
    }
}
