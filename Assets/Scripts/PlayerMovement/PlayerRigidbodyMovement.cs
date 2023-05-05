using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Essentials.Reference_Variables.Variables;

public class PlayerRigidbodyMovement : MonoBehaviour
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

    private Rigidbody rb;
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

    private FloatVariable jumpInput
    {
        get
        {
            return jumpInputVariable;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove()
    {
        Debug.Log("moving");
        moveInput = movementInput;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log(context.action.triggered + "normal");
        Debug.Log(jumpInput + "new");
        jumped = jumpInput;//context.action.triggered;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTarget();
    }

    private void MoveTarget()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        rb.velocity = move * playerSpeed;

        if (jumped && groundedPlayer)
        {
            //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        //rb.velocity.y += gravityValue * Time.deltaTime;
    }

    public void CheckGrounded()
    {
        float groundDistance = 0.1f;
        Vector3 position = transform.position;
        groundedPlayer = Physics.Raycast(position, Vector3.down, groundDistance);
    }
}
