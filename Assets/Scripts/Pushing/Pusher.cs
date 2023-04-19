using System;
using Essentials.Reference_Variables.Variables;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] private FloatVariable shovingInputVariable;
    [SerializeField] private string shovingEventKey;

    private float shovingInput
    {
        get
        {
            return shovingInputVariable.Value;
        }
    }
    
    private void Awake()
    {
        shovingInputVariable.ValueChanged += OnShovingValueChanged;
    }

    private void OnShovingValueChanged(object sender, EventArgs e)
    {
        if (shovingInput == 0)
            return;
        EventManager.TriggerEvent(shovingEventKey, new ShoveEvent());
        throw new NotImplementedException();
    }
}
