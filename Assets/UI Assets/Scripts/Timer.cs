using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 120f;

    public TextMeshProUGUI timeText;

    public bool startTimer = false;


    void Update()
    {
        if (startTimer)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
                startTimer = false;
            }

            DisplayTime(timeValue);
        }

    }

    void DisplayTime(float timeToDisplay)
    {

        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

    }

    public void StartTimer()
    {
        startTimer = true;
    }
    
}
