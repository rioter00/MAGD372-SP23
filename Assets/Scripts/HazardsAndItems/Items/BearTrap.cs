using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BearTrap : Item
{
    [SerializeField] GameObject bearTrap;

    public override void Activate()
    {
        Vector3 bottomPosition = transform.TransformPoint(0, transform.position.y - 2.35f, 0);
        Instantiate(bearTrap, bottomPosition, Quaternion.identity);
    }
}
