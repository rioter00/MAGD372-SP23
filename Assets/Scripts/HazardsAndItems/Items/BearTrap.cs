using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : Item
{
    public override void Activate()
    {
        Instantiate(Resources.Load("BearTrapGO"), transform);
    }
}
