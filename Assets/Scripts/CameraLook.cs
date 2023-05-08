using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] float minViewDistance = 25f;
    [SerializeField] Transform playerBody;
    public float Sensitivity = 100f;

    float xRotation = 0f;

    public Vector2 input;

    // Update is called once per frame
    void Update()
    {
        float mouseX = input.x * Sensitivity * Time.deltaTime;
        float mouseY = input.y * Sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, minViewDistance);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
