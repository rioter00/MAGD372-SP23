using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;
using UnityEngine;

public class ToggleGameObjectOnKeyPress : MonoBehaviour
{
    public GameObject objected;
    public GameObject rigidBodyFPSController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            Cursor.visible = !objected.activeSelf; // Enables the UI and disables the character controls
            if (!objected.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            rigidBodyFPSController.GetComponent<RigidbodyFirstPersonController>().enabled = objected.activeSelf;
            objected.SetActive(!objected.activeSelf);
        }
    }
}
