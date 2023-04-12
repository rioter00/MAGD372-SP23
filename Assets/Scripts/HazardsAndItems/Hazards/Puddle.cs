using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Puddle : MonoBehaviour
{
    [SerializeField] float spinTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform player = other.GetComponent<Transform>();
            Spin(player);
        }
    }

    void Spin(Transform player)
    {
        player.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, -transform.rotation.eulerAngles, spinTime);
    }
}
