using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Spring : MonoBehaviour // ISpill?
{
    [SerializeField] float upForce;
    [SerializeField] float forwardForce;
    [SerializeField] MeshRenderer inactiveSpring;
    [SerializeField] MeshRenderer activeSpring;

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
        StartCoroutine(Extend());
        player.AddForce(Vector3.up * upForce + Vector3.forward * forwardForce);
        //Spill();
    }

    IEnumerator Extend()
    {
        inactiveSpring.enabled = false;
        activeSpring.enabled = true;

        yield return new WaitForSeconds(1);

        activeSpring.enabled = false;
        inactiveSpring.enabled = true;
    }
}
