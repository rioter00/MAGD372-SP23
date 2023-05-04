using UnityEngine;
using UnityEngine.InputSystem;
using Essentials.Reference_Variables.Variables;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    
    [SerializeField]//movement scriptable object
    private Vector2Variable movementInputVariable;

    [SerializeField]//Jump scriptable object
    private FloatVariable jumpInputVariable;

    private Rigidbody controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 moveInput = Vector2.zero;
    private bool jumped = false;

    private Vector2 movementInput
    {
        get
        {
            return movementInputVariable.Value;
        }
    }

    private float jumpInput
    {
        get
        {
            return jumpInputVariable.Value;
        }
    }

    private void Awake()
    {
        jumpInputVariable.ValueChanged += OnJump;
    }

    private void Start()
    {
        controller = gameObject.GetComponent<Rigidbody>();
    }

    public void OnMove()
    {
        moveInput = movementInput;
    }

    public void OnJump(object sender, EventArgs args)
    {
        Debug.Log("Jump");
        jumped = jumpInput == 1;
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        if (groundedPlayer && playerVelocity.y < 0)
        {
            //playerVelocity.y = 0f;
        }

        playerVelocity = new Vector3(movementInput.x, controller.velocity.y, movementInput.y);
        //controller.velocity = (move * playerSpeed);

        //remove possibly
        if (playerVelocity != Vector3.zero)
        {
            var move = Vector3.ProjectOnPlane(playerVelocity, Vector3.up);
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        controller.velocity = (playerVelocity);
    }

    public void CheckGrounded()
    {
        float groundDistance = 0.1f;
        Vector3 position = transform.position;
        groundedPlayer = Physics.Raycast(position, Vector3.down, groundDistance);
    }

    void OnDestroy()
    {
        jumpInputVariable.ValueChanged -= OnJump;

    }
}




//include this stuff later

//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    [Header("Movement")]
//    public float moveSpeed;

//    public float groundDrag;

//    public float jumpForce;
//    public float jumpCooldown;
//    public float airMultiplier;
//    bool readyToJump = true;

//    [Header("KeyBinds")]//change this out for playIntput Controller
//    public KeyCode jumpKey = KeyCode.Space;

//    [Header("Ground Check")]
//    public float playerHeight;
//    public LayerMask whatIsGround;
//    bool grounded;




//    public Transform orientation;

//    float horizontalInput;
//    float verticalInput;

//    Vector3 moveDirection;

//    Rigidbody rb;

//    private void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        rb.freezeRotation = true;
//    }
//    public void Update()
//    {
//        //bool check to see if youre grounded. raycast half your height then some.
//        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);


//        MyInput();
//        Speedcontrol();

//        //handle drag
//        if (grounded)
//            rb.drag = groundDrag;
//        else
//            rb.drag = 0;
//    }

//    private void FixedUpdate()
//    {
//        MovePlayer();
//    }

//    private void MyInput()
//    {
//        horizontalInput = Input.GetAxisRaw("Horizontal");
//        verticalInput = Input.GetAxisRaw("Vertical");

//        if (Input.GetKey(jumpKey) && readyToJump && grounded)
//        {

//            readyToJump = false;

//            Jump();

//            Invoke(nameof(ResetJump), jumpCooldown);//this is jump cooldown
//        }

//    }

//    private void MovePlayer()
//    {
//        //to move in the direction youre looking
//        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
//        //move direction will be replayced by the PlayerInput controller keybind. (Dont have a controller to test currently)

//        if(grounded)
//            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
//        else if (!grounded)
//            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);


//    }

//    private void Speedcontrol()
//    {
//        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

//        //limit velocity 
//        if(flatVel.magnitude > moveSpeed)
//        {
//            Vector3 limitedVel = flatVel.normalized * moveSpeed;
//            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
//        }

//    }

//    private void Jump()
//    {
//        //reset y velocity
//        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

//        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

//    }
//    private void ResetJump()
//    {
//        readyToJump = true;
//    }
//}