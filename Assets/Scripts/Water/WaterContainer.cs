using System.Collections;
using Essentials.Reference_Variables.References;
using UnityEngine;

public class WaterContainer : MonoBehaviour
{
    [SerializeField] private FloatReference waterReference;
    [SerializeField] private FloatReference maxFillAmountReference;
    [SerializeField] private FloatReference leakRateReference;

    protected float water
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
    protected float maxFillAmount
    {
        get
        {
            return maxFillAmountReference.Value;
        }
    }
    protected float leakRate
    {
        get
        {
            return leakRateReference.Value;
        }
    }
    protected float fillPerSecond;

    private IEnumerator Start()
    {
        if (leakRate == 0)
            yield break;
        while (true)
        {
            yield return new WaitForSeconds(1);
        }
    }

}
