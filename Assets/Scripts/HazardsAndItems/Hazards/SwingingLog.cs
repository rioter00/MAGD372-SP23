using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingLog : MonoBehaviour
{
    [SerializeField] float maxDelfection = 30;
    [SerializeField] float rateOfSwing = 2;

    private void Update()
    {
        float angle = maxDelfection * Mathf.Sin(Time.time * rateOfSwing);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
