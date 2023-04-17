using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Puddle : MonoBehaviour
{
    [SerializeField] float spinForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody player = other.GetComponent<Rigidbody>();
            Spin(player);
        }
    }

    void Spin(Rigidbody player)
    {
        //player.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, transform.rotation.eulerAngles + new Vector3(0, 360, 0), spinTime);
        
        //Quaternion rot = Quaternion.Euler(0, spinTime, 0);
        //player.MoveRotation(player.rotation * rot);

        //player.AddTorque(Vector3.up * spinForce);
    }
}
