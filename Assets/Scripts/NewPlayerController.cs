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

    private Rigidbody rb;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    [SerializeField] private CameraLook cameraLook;

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
        jumped = context.ReadValueAsButton();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {

    }

    public void OnShove(InputAction.CallbackContext context)
    {

    }

    public void OnPowerupOne(InputAction.CallbackContext context)
    {

    }

    public void OnPowerupTwo(InputAction.CallbackContext context)
    {

    }

    public void OnPause(InputAction.CallbackContext context)
    {

    }

    public void OnSpill(InputAction.CallbackContext context)
    {

    }

    void FixedUpdate()
    {
        CheckGrounded();
        if (groundedPlayer && playerVelocity.y < 0)
        {
            //playerVelocity.y = 0f;
        }

        playerVelocity = new Vector3(movementInput.x, rb.velocity.y, movementInput.y);

        //remove possibly
        //if (playerVelocity != Vector3.zero)
        //{
        //    var move = Vector3.ProjectOnPlane(playerVelocity, Vector3.up);
        //    gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        rb.velocity = (playerVelocity * playerSpeed);
    }

    public void CheckGrounded()
    {
        float groundDistance = 0.1f;
        Vector3 position = transform.position;
        groundedPlayer = Physics.Raycast(position, Vector3.down, groundDistance);
    }
}
