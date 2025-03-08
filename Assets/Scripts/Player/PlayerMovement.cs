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

    private readonly float gravity = -9.81f * 3;
    private Vector3 velocity;

    public bool onGround;
    public Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    private float jumpTimer;

    [HideInInspector]
    public Vector3 direction;
    private Vector3 lastPos;

    //Knockback
    private Vector3 KnockbackForce;

    private float speedReductionTimer;
    private float speedReduction;


    //sliding from steep surface
    private bool canMove = true;
    private Vector3 slidingDirection;

    //aiming
    public bool isAiming = false;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        Application.targetFrameRate = 120;
        lastPos = transform.position;
    }

    void Update()
    {
        onGround = Physics.CheckSphere(groundCheck.position, 0.6f, groundMask);
        CheckStandingSurface();

        RaycastHit hit;
        Physics.Raycast(groundCheck.position, Vector3.down, out hit, Mathf.Infinity, groundMask);

        if(onGround && velocity.y < 0)
        {
            velocity.y = -2;
        }


        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Move();

        Sprint();
        if(!locked && canMove) {
            controller.Move(move.normalized * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && (hit.distance < 1 || onGround) && jumpTimer < Time.time)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                jumpTimer = Time.time + 1;
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

    
    private void Sprint()
    {
        if(speedReductionTimer > Time.time)
        {
            speed = speedReduction;
        }
        else if(y > 0 && Input.GetKey(KeyCode.LeftShift) && !isAiming)
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

    private void CheckStandingSurface()
    {
        canMove = true;
        if(onGround){
            
            if(IsToSteep(new Vector3(0, 0, 0)) && IsToSteep(new Vector3(0.5f, 0, 0)) && IsToSteep(new Vector3(-0.5f, 0, 0)) && 
            IsToSteep(new Vector3(0, 0, 0.5f)) && IsToSteep(new Vector3(0, 0, -0.5f)))
            {
                canMove = false;
                controller.Move(new Vector3(slidingDirection.x * 0.05f, -slidingDirection.y * 0.05f, slidingDirection.z * 0.05f));
            }
        }
    }

    private bool IsToSteep(Vector3 offset)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + offset, Vector3.down, out hit, Mathf.Infinity, groundMask);
        Debug.DrawLine(hit.point, hit.point + hit.normal);
        if(hit.transform != null && hit.normal.y < 0.5f)
        {
            slidingDirection = hit.normal;
            return true;
        }
        return false;
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

    public void Knockback(Vector3 enemy, float force)
    {
        KnockbackForce = (transform.position - enemy).normalized * force;
    }

    public void ReduceSpeed(float speed, float time)
    {
        speedReduction = speed;
        speedReductionTimer = Time.time + time;
    }
}
