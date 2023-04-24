using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        //Replace this with anything you would need for movement
        movementInput = context.ReadValue<Vector2>();
    }

    public void PlayerCamera(InputAction.CallbackContext context)
    {
        Debug.Log("Camera is moving");
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        //Replace this with anything you would need for jumping
        jumped = context.action.triggered;
    }

    public void PlayerInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interacting with object");
    }

    public void PlayerShove(InputAction.CallbackContext context)
    {
        Debug.Log("Shoving");
    }

    public void PlayerSpill(InputAction.CallbackContext context)
    {
        Debug.Log("Spilling water");
    }

    public void PlayerPowerupOne(InputAction.CallbackContext context)
    {
        Debug.Log("Activating Powerup One");
    }

    public void PlayerPowerupTwo(InputAction.CallbackContext context)
    {
        Debug.Log("Activating Powerup Two");
    }

    public void PlayerPause(InputAction.CallbackContext context)
    {
        Debug.Log("The game would pause here");
    }

    void Update()
    {
        //Replace this with anything you would need for player actions
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}