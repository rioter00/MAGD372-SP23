using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;
using UnityEngine;

public class RadioScript : MonoBehaviour // C# Script
{
    public GameObject rigidBodyFPSController;
    public AudioSource m_MyAudioSource;
    public Canvas userInterface;
    public TMP_Dropdown userDropdown;
    public Button playButton;
    public Button stopButton;
    AudioClip song;
    string[] music;
    string[] musicTitles;

    void Start()
    {
        m_MyAudioSource.Pause();
        music = Directory.GetFiles(@"F:\UW STOUT\CS 326\Research Project - Backup\Assets\Resources", "*.mp3");
        musicTitles = new string[music.Length];
        for (int i = 0; i < music.Length; i++)
        {
            musicTitles[i] = Path.GetFileNameWithoutExtension(music[i]);
        }
        song = (AudioClip)Resources.Load(musicTitles[0]);
        m_MyAudioSource.clip = song;
        Cursor.lockState = CursorLockMode.Locked;

        userInterface.GetComponent<Canvas>().enabled = false;

        // userDropdown is null for some reason
        List<string> lst = musicTitles.OfType<string>().ToList();
        userDropdown.ClearOptions();
        userDropdown.AddOptions(lst);
        userDropdown.RefreshShownValue();

        playButton.onClick.AddListener(PlayTaskOnClick);
        stopButton.onClick.AddListener(StopTaskOnClick);
    }

    void OnMouseDown()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        userInterface.GetComponent<Canvas>().enabled = true;
        rigidBodyFPSController.GetComponent<RigidbodyFirstPersonController>().enabled = false; // Disables the user controls
    }

    public void PlayTaskOnClick()
    {
        song = (AudioClip)Resources.Load(musicTitles[userDropdown.value]); // Updates the song
        m_MyAudioSource.clip = song; // Assigns the song to the audioclip
        m_MyAudioSource.Play(); // Plays the audioclip
        for (int i = 0; i < musicTitles.Length; i++)
        {
            Debug.Log(musicTitles[i]);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        userInterface.GetComponent<Canvas>().enabled = false;
        rigidBodyFPSController.GetComponent<RigidbodyFirstPersonController>().enabled = true; // Reenables the user controls
    }

    public void StopTaskOnClick()
    {
        m_MyAudioSource.Stop(); // Plays the audioclip
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        userInterface.GetComponent<Canvas>().enabled = false;
        rigidBodyFPSController.GetComponent<RigidbodyFirstPersonController>().enabled = true; // Reenables the user controls
    }
}