using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sand : MonoBehaviour
{
    [SerializeField] float drag;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody player = other.GetComponent<Rigidbody>();
            Slow(player);
        }
    }

    void Slow(Rigidbody player)
    {
        Vector3 oppositeVelocity = -player.velocity;
        float forceMagnitude = drag * player.velocity.sqrMagnitude;
        player.AddForce(oppositeVelocity.normalized * forceMagnitude, ForceMode.Force);
    }
}
