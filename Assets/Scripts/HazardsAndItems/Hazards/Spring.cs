using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Spring : MonoBehaviour // ISpill?
{
    [SerializeField] float upForce;
    [SerializeField] float forwardForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody player = other.GetComponent<Rigidbody>();
            Bounce(player);
        }
    }

    void Bounce(Rigidbody player)
    {
        player.AddForce(Vector3.up * upForce + Vector3.forward * forwardForce);
        //Spill();
    }
}
