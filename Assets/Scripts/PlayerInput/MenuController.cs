using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{

    public void MenuUp(InputAction.CallbackContext context)
    {
        Debug.Log("Moving menu cursor up");
    }

    public void MenuDown(InputAction.CallbackContext context)
    {
        Debug.Log("Moving menu cursor down");
    }

    public void MenuLeft(InputAction.CallbackContext context)
    {
        Debug.Log("Moving menu cursor left");
    }

    public void MenuRight(InputAction.CallbackContext context)
    {
        Debug.Log("Moving menu cursor right");
    }

    public void MenuSelect(InputAction.CallbackContext context)
    {
        Debug.Log("Selecting current option");
    }

    public void MenuBack(InputAction.CallbackContext context)
    {
        Debug.Log("Going back one");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
