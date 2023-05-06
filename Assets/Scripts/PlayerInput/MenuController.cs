using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Essentials.Reference_Variables.Variables;

public class MenuController : MonoBehaviour
{

    [SerializeField] private InputValues player_one;
    [SerializeField] private InputValues player_two;
    [SerializeField] private MenuManager menuManager;

    [System.Serializable]
    public class InputValues
    {
        [SerializeField] private FloatVariable up_input;
        [SerializeField] private FloatVariable down_input;
        [SerializeField] private FloatVariable left_input;
        [SerializeField] private FloatVariable right_input;
        [SerializeField] private FloatVariable select_input;
        [SerializeField] private FloatVariable back_input;

        public float Up
        {
            set
            {
                up_input.Value = value;
            }
        }

        public float Down
        {
            set
            {
                down_input.Value = value;
            }
        }

        public float Left
        {
            set
            {
                left_input.Value = value;
            }
        }

        public float Right
        {
            set
            {
                right_input.Value = value;
            }
        }

        public float Select
        {
            set
            {
                select_input.Value = value;
            }
        }

        public float Back
        {
            set
            {
                back_input.Value = value;
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

    public void MenuUp(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.Up = input;
        }
        else if (device == player2)
        {
            player_two.Up = input;
        }
    }

    public void MenuDown(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.Down = input;
        }
        else if (device == player2)
        {
            player_two.Down = input;
        }
    }

    public void MenuLeft(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.Left = input;
        }
        else if (device == player2)
        {
            player_two.Left = input;
        }
    }

    public void MenuRight(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (device == player1)
        {
            player_one.Right = input;
        }
        else if (device == player2)
        {
            player_two.Right = input;
        }
    }

    public void MenuSelect(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (context.performed)
        {
            if (device == player1)
            {
                player_one.Select = input;
            }
            else if (device == player2)
            {
                player_two.Select = input;
            }
        }
    }

    public void MenuBack(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        float input = context.ReadValue<float>();
        if (context.performed)
        {
            if (device == player1)
            {
                player_one.Back = input;
                menuManager.BackButton(menuManager.GetCurrentActivePanel());
            }
            else if (device == player2)
            {
                player_two.Back = input;
            }
        }
    }
}
