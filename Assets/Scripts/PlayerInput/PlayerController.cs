using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public void PlayerMove(InputAction.CallbackContext context)
    {
        
    }

    public void PlayerCamera(InputAction.CallbackContext context)
    {
        Debug.Log("Camera is moving");
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        
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
}