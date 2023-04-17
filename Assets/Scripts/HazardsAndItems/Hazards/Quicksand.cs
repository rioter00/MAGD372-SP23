using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Quicksand : MonoBehaviour
{
    [SerializeField] float pullSpeed;
    [SerializeField] Transform center;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody player = other.GetComponent<Rigidbody>();
            Pull(player);
        }
    }

    void Pull(Rigidbody player)
    {
        //player.position = Vector3.MoveTowards(player.position, new Vector3(center.position.x, player.position.y, center.position.z), pullSpeed);
        Vector3 direction = center.position - player.position;
        direction.Normalize();
        player.AddForce(direction * pullSpeed, ForceMode.Force);
    }
}
