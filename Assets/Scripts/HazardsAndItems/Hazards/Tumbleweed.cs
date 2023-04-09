using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tumbleweed : MonoBehaviour
{
    [SerializeField] float pushForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody player = collision.collider.GetComponent<Rigidbody>();
            Push(player);
        }
    }

    void Push(Rigidbody player)
    {
        player.AddForce(-pushForce, pushForce, 0, ForceMode.Impulse); // might have to check whether it should be x or z, also which direction tumbleweed is facing
    }
}
