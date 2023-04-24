using UnityEngine;

public class Bucket : WaterContainer
{
    [SerializeField] private string bucketFillEventKey;
    
    private void Awake()
    {
        EventManager.AddListener<BucketFillEvent>(bucketFillEventKey, FillBucket);
    }

    public void FillBucket(float waterAmount)
    {
        water += waterAmount;
    }

    private void FillBucket(BucketFillEvent obj)
    {
        water += obj.WaterAmount;
    }
    
    private void OnDestroy()
    {
        EventManager.RemoveListener<BucketFillEvent>(bucketFillEventKey, FillBucket);
    }
}

public class BucketFillEvent
{
    public BucketFillEvent(float waterAmount)
    {
        WaterAmount = waterAmount;
    }
    
    public float WaterAmount { get; }
}