using System;
using System.Collections;
using Essentials.Reference_Variables.References;
using Essentials.Reference_Variables.Variables;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cup : WaterContainer
{
    [SerializeField] private FloatVariable bucketFillInputVariable;
    [SerializeField] private string bucketFillEventKey;
    [SerializeField] private FloatReference bucketFillRateReference;
    [SerializeField] private FloatReference bucketRangeReference;

    private float bucketFillInput;
    private float bucketFillRate
    {
        get
        {
            return bucketFillRateReference.Value;
        }
    }
    private float bucketRange
    {
        get
        {
            return bucketRangeReference.Value;
        }
    }

    private Coroutine bucketFilling;
    private Coroutine waterCollection;

    private void Awake()
    {
        bucketFillInputVariable.ValueChanged += InputHandler;

        var body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        body.useGravity = false;
    }

    private void InputHandler(object sender, EventArgs e)
    {
        if (bucketFillInput == 0 && bucketFilling != null)
        {
            StopCoroutine(bucketFilling);
            bucketFilling = null;
            return;
        }
        FindBucket();
    }

    private void FindBucket()
    {

        var results = Array.Empty<Collider>();
        int size = Physics.OverlapSphereNonAlloc(transform.position, bucketRange, results);
        for (int i = 0; i < size; i++)
        {
            var bucket = results[i].gameObject.GetComponent<Bucket>();
            if (!bucket)
                continue;
            bucketFilling = StartCoroutine(FillBucket(bucket));
            return;
        }
    }

    private IEnumerator FillBucket(Bucket bucket)
    {
        for (;;)
        {
            //EventManager.TriggerEvent(bucketFillEventKey, new BucketFillEvent(Mathf.Min(water, bucketFillRate)));
            bucket.FillBucket(Mathf.Min(water, bucketFillRate));
            water -= Mathf.Min(water, bucketFillRate);
            yield return new WaitForSeconds(1);
        }
    }

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
