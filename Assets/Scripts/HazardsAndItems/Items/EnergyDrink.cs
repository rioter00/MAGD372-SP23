using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : Item
{
    //PlayerStats stats;
    Renderer water; // for changing water color

    private void Awake()
    {
        //stats = GetComponent<PlayerStats>();
        water = GameObject.FindGameObjectWithTag("Water").GetComponent<Renderer>();
    }

    public override void Activate()
    {
        //stats.SetSpeed(stats.GetSpeed() * 2);
        //stats.SetJump(stats.GetJump() * 2);

        water.material.SetColor("_Color", Color.green);

        StartCoroutine(StartDeactivation());
    }

    IEnumerator StartDeactivation()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this);
    }
}
