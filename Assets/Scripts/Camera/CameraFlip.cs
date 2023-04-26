using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour
{
    public Camera player2Camera;
    private void Start()
    {
        Matrix4x4 mat = player2Camera.projectionMatrix;
        mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        player2Camera.projectionMatrix = mat;
    }
}
