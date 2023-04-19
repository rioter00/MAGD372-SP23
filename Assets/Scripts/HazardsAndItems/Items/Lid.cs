using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lid : Item
{
    //Water water; // essentially PlayerHealth

    public override void Activate()
    {
        //water = GetComponent<Water>();
        //water.SetActive(false); // for this to work, water bar (health bar) might need a "if null"
        Debug.Log("Lid activated!"); // *remove*
        StartCoroutine(StartDeactivation());
    }

    IEnumerator StartDeactivation()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

    void Deactivate()
    {
        //water.SetActive(true);
        Destroy(this);
    }
}
