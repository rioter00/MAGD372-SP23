using UnityEngine;
using UnityEngine.InputSystem;

//use this for scriptable object Vect2Variables
using Essentials.Reference_Variables.Variables;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    //from lucas example
    [SerializeField] 
    private Vector2Variable movementInputVariable; // thsi should be a scriptable object I guess?

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 movementInput33 = Vector2.zero;
    private bool jumped = false;

    private Vector2 movementInput
    {
        get
        {
            return movementInputVariable.Value;
        }
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = movementInput;//context.ReadValue<Vector2>();
        Debug.Log(movementInput + "movement input");
        Debug.Log(context.ReadValue<Vector2>() + "context input");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        //remove possibly
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}




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