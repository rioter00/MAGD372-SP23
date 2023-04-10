using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Quicksand : MonoBehaviour
{
    [SerializeField] float pullSpeed;
    Transform center;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform player = other.GetComponent<Transform>();
            Pull(player);
        }
    }

    void Pull(Transform player)
    {
        player.position = Vector3.MoveTowards(player.position, center.position, pullSpeed);
    }
}
