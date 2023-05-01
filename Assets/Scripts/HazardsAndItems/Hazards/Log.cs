using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] bool alternativeRotate;

    void Update()
    {
        if (!alternativeRotate)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
        }
        else
        {
            transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
        }
    }
}
