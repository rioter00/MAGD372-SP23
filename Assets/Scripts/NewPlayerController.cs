using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    public float groundDrag;

    private Rigidbody rb;

    private Vector3 moveDirection;


    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    [SerializeField] private CameraLook cameraLook;

    [SerializeField] private GameObject playerPausePanel;

    //private InputActionAsset inputAsset;
    //private InputActionMap player;
    //private InputAction move;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        cameraLook.input = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        {
            jumped = context.ReadValueAsButton();
            DoJump();
            //Debug.Log("jump pressed");
        }
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("interact pressed");
        }
        
    }

    public void OnShove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("shove pressed");
        }
    }

    public void OnPowerupOne(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("trigger left pressed");
        }
    }

    public void OnPowerupTwo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("trigger right pressed");
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("pasued pressed");
        }
    }

    public void OnSpill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("spill pressed");
        }
    }

    //private void Awake()
    //{
    //    inputAsset = this.GetComponent<PlayerInput>().actions;
    //    player = inputAsset.FindActionMap("Player");
    //}

    //private void OnEnable()
    //{
    //    player.FindAction("Jump").started += OnJump;
    //    move = player.FindAction("Movement");
    //    player.Enable();
    //}

    //private void OnDisable()
    //{
    //    player.FindAction("Jump").started -= OnJump;
    //    player.Disable();
    //}

    void FixedUpdate()
    {
        CheckGrounded();

        SpeedControl();
        MovePlayer();


        if (groundedPlayer)
        {
            rb.drag = groundDrag;
        }
        else
            rb.drag = 0;
    }

    private void MovePlayer()
    {
        if (movementInput == Vector2.zero)
            return;
        moveDirection = transform.forward * movementInput.y + transform.right * movementInput.x;

        rb.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void DoJump()
    {
        if (jumped && groundedPlayer)
        {
            Debug.Log("jumping");
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }

    public void CheckGrounded()
    {
        float groundDistance = 0.1f;
        Vector3 position = transform.position;
        groundedPlayer = Physics.Raycast(position, Vector3.down, groundDistance);
    }
}
