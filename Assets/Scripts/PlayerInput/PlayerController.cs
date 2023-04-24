using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Essentials.Reference_Variables.Variables;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2Variable move_input;
    [SerializeField] private Vector2Variable camera_input;
    [SerializeField] private FloatVariable jump_input;
    [SerializeField] private FloatVariable interact_input;
    [SerializeField] private FloatVariable shove_input;
    [SerializeField] private FloatVariable spill_input;
    [SerializeField] private FloatVariable powerup_one_input;
    [SerializeField] private FloatVariable powerup_two_input;
    [SerializeField] private FloatVariable pause_input;


    public void PlayerMove(InputAction.CallbackContext context)
    {
        move_input.Value = context.ReadValue<Vector2>();
    }

    public void PlayerCamera(InputAction.CallbackContext context)
    {
        camera_input.Value = context.ReadValue<Vector2>();
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        jump_input.Value = context.ReadValue<float>();
    }

    public void PlayerInteract(InputAction.CallbackContext context)
    {
        interact_input.Value = context.ReadValue<float>();
    }

    public void PlayerShove(InputAction.CallbackContext context)
    {
        shove_input.Value = context.ReadValue<float>();
    }

    public void PlayerSpill(InputAction.CallbackContext context)
    {
        spill_input.Value = context.ReadValue<float>();
    }

    public void PlayerPowerupOne(InputAction.CallbackContext context)
    {
        powerup_one_input.Value = context.ReadValue<float>();
    }

    public void PlayerPowerupTwo(InputAction.CallbackContext context)
    {
        powerup_two_input.Value = context.ReadValue<float>();
    }

    public void PlayerPause(InputAction.CallbackContext context)
    {
        pause_input.Value = context.ReadValue<float>();
    }
}