using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Faucet : MonoBehaviour
{
    [SerializeField] private float fillPerSecond;

    public float FillPerSecond
    {
        get
        {
            return fillPerSecond;
        }
    }
}
