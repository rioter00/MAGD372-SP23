using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected float duration;
    //PlayerInput input;

    void Start()
    {
        //input = GetComponent<PlayerInput>();
    }

    public abstract void Activate();
}
