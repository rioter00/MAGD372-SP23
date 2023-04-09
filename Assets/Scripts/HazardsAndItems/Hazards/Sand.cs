using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sand : MonoBehaviour
{
    [SerializeField] float newDrag;
    float originalDrag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody player = other.GetComponent<Rigidbody>();
            originalDrag = player.drag;
            Slow(player);
        }
    }

    void Slow(Rigidbody player)
    {
        player.drag = newDrag;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody player = other.GetComponent<Rigidbody>();
            ResetDrag(player);
        }
    }

    void ResetDrag(Rigidbody player)
    {
        player.drag = originalDrag;
    }
}
