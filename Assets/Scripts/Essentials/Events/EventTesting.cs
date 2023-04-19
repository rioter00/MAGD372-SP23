using System;
using System.Collections;
using UnityEngine;

public class EventTesting : MonoBehaviour
{
    private int counter = 0;
    
    private void Awake()
    {
        EventManager.AddListener<EventDemo>("TestEvents", EventTest);
    }

    private void EventTest(EventDemo args)
    {
        Debug.Log(args.number);
    }

    private IEnumerator Start()
    {
        while (true)
        {
            var args = new EventDemo(counter++);
            EventManager.TriggerEvent("TestEvents", args);
            yield return new WaitForSeconds(1);
        }
    }
}

public class EventDemo
{
    public EventDemo(float num)
    {
        number = num;
    }
    
    public float number;
}
