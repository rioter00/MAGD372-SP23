using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Spiller : MonoBehaviour
{
    private void Awake()
    {
        var body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        body.useGravity = false;
        body.collisionDetectionMode = CollisionDetectionMode.Discrete;
        body.interpolation = RigidbodyInterpolation.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<Cup>(out var cup);
        if (!cup)
            return;
        cup.SpillWater();
    }
}
