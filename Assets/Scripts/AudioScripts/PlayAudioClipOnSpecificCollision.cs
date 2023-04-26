using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayAudioClipOnSpecificCollision : MonoBehaviour // C# Script
{
    public AudioSource m_MyAudioSource;
    AudioClip song;

    void Start()
    {
        m_MyAudioSource.Pause(); // Pauses the designated audio source on start to keep the audio from immediately playing
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.gameObject.name == "RigidBodyFPSController")
        {
            PlayTaskOnClick();
        }
    }

    public void PlayTaskOnClick()
    {
        m_MyAudioSource.Play(); // Plays the audioclip
    }
}
