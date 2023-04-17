using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayAudioClipOnClick : MonoBehaviour // C# Script
{
    public GameObject rigidBodyFPSController;
    public AudioSource m_MyAudioSource;
    AudioClip song;

    void Start()
    {
        m_MyAudioSource.Pause(); // Pauses the designated audio source on start to keep the audio from immediately playing
    }

    void OnMouseDown()
    {
        PlayTaskOnClick();
    }

    public void PlayTaskOnClick()
    {
        m_MyAudioSource.Play(); // Plays the audioclip
    }
}
