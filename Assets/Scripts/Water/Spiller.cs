using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Spiller : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<Cup>(out var cup);
        if (!cup)
            return;
        cup.SpillWater();
    }
}
