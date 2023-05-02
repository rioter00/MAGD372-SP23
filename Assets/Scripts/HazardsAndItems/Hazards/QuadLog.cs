using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadLog : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
    }
}
