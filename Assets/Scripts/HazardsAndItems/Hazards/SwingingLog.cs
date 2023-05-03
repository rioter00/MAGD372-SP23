using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingLog : MonoBehaviour
{
    [SerializeField] float maxDelfection = 30;
    [SerializeField] float rateOfSwing = 2;
    [SerializeField] bool alternativeDir;
    [SerializeField] Transform log;

    private void Update()
    {
        float angle = maxDelfection * Mathf.Sin(Time.time * rateOfSwing);
        
        if (!alternativeDir)
        {
            log.rotation = Quaternion.Euler(0, 90, 0);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            log.rotation = Quaternion.Euler(0, 0, 0);
            transform.localRotation = Quaternion.Euler(angle, 0, 0);
        }
    }
}
