using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Cactus : MonoBehaviour // IDrain? ISpill? IWater?
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Spill();
            //Drain();

            Debug.Log("YEEEEEEEEEEEOOOOOOOOOOUUUUCHH!!!");
        }
    }
}
