using System;
using System.Collections;
using Essentials.Reference_Variables.References;
using UnityEngine;

public class WaterContainer : MonoBehaviour
{
    [SerializeField] private FloatReference waterReference;
    [SerializeField] private FloatReference maxFillAmountReference;

    private float water
    {
        get
        {
            return waterReference.Value;
        }
        set
        {
            waterReference.Value = value;
        }
    }
    private float maxFillAmount
    {
        get
        {
            return maxFillAmountReference.Value;
        }
    }
    private float fillPerSecond;

    private Coroutine waterCollection;

    private IEnumerator CollectWater()
    {
        for (;;)
        {
            if (water < maxFillAmount)
                water += fillPerSecond * Time.deltaTime;
            else
                water = maxFillAmount;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var faucet = other.gameObject.GetComponent<Faucet>();
        if (!faucet)
            return;

        fillPerSecond = faucet.FillPerSecond;
        waterCollection = StartCoroutine(CollectWater());
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(waterCollection);
    }
}
