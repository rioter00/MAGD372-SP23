using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Essentials.Reference_Variables.Variables;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputValues player_one;
    [SerializeField] private InputValues player_two;

    [System.Serializable]
    public class InputValues
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
        [SerializeField] bool flipped;

        public Vector2 Move
        {
            set
            {
                if (flipped)
                {
                    value.x = -value.x;
                }
                move_input.Value = value;
            }
        }

        public Vector2 Camera
        {
            set
            {
                if (flipped)
                {
                    value.x = -value.x;
                }
                camera_input.Value = value;
            }
        }

        public float Jump
        {
            set
            {
                jump_input.Value = value;
            }
        }

        public float Interact
        {
            set
            {
                interact_input.Value = value;
            }
        }

        public float Shove
        {
            set
            {
                shove_input.Value = value;
            }
        }

        public float Spill
        {
            set
            {
                spill_input.Value = value;
            }
        }

        public float PowerupOne
        {
            set
            {
                powerup_one_input.Value = value;
            }
        }

        public float PowerupTwo
        {
            set
            {
                powerup_two_input.Value = value;
            }
        }

        public float Pause
        {
            set
            {
                pause_input.Value = value;
            }
        }
    }

    private Gamepad player1;
    private Gamepad player2;

    private void Start()
    {
        foreach (var gamepad in Gamepad.all)
        {
            if (player1 == null)
            {
                player1 = gamepad;
            }
            else
            {
                player2 = gamepad;
            }
        }
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        Vector2 input = context.ReadValue<Vector2>();
        if (device == player1)
        {
            player_one.Move = input;
        }
        else if (device == player2) {
            player_two.Move = input;
        }
    }

    public void PlayerCamera(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        Vector2 input = context.ReadValue<Vector2>();
        if (device == player1)
        {
            player_one.Camera = input;
        }
        else if (device == player2)
        {
            player_two.Camera = input;
        }
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.Jump = input;
        }
        else if (device == player2)
        {
            player_two.Jump = input;
        }
    }

    public void PlayerInteract(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.Interact = input;
        }
        else if (device == player2)
        {
            player_two.Interact = input;
        }
    }

    public void PlayerShove(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.Shove = input;
        }
        else if (device == player2)
        {
            player_two.Shove = input;
        }
    }

    public void PlayerSpill(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.Spill = input;
        }
        else if (device == player2)
        {
            player_two.Spill = input;
        }
    }

    public void PlayerPowerupOne(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.PowerupOne = input;
        }
        else if (device == player2)
        {
            player_two.PowerupOne = input;
        }
    }

    public void PlayerPowerupTwo(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.PowerupTwo = input;
        }
        else if (device == player2)
        {
            player_two.PowerupTwo = input;
        }
    }

    public void PlayerPause(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.Pause = input;
        }
        else if (device == player2)
        {
            player_two.Pause = input;
        }
    }
}