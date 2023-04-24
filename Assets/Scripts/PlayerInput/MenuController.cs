using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Essentials.Reference_Variables.Variables;

public class MenuController : MonoBehaviour
{
    [SerializeField] private FloatVariable up_input;
    [SerializeField] private FloatVariable down_input;
    [SerializeField] private FloatVariable left_input;
    [SerializeField] private FloatVariable right_input;
    [SerializeField] private FloatVariable select_input;
    [SerializeField] private FloatVariable back_input;
    public void MenuUp(InputAction.CallbackContext context)
    {
        up_input.Value = context.ReadValue<float>();
    }

    public void MenuDown(InputAction.CallbackContext context)
    {
        down_input.Value = context.ReadValue<float>();
    }

    public void MenuLeft(InputAction.CallbackContext context)
    {
        left_input.Value = context.ReadValue<float>();
    }

    public void MenuRight(InputAction.CallbackContext context)
    {
        right_input.Value = context.ReadValue<float>();
    }

    public void MenuSelect(InputAction.CallbackContext context)
    {
        select_input.Value = context.ReadValue<float>();
    }

    public void MenuBack(InputAction.CallbackContext context)
    {
        back_input.Value = context.ReadValue<float>();
    }
}
