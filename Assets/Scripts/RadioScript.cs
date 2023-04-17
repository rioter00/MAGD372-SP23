/*
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
        m_MyAudioSource.Pause(); // Pauses the designated audio source on start to keep the audio from immediately playing
        music = Directory.GetFiles(@"F:\UW STOUT\CS 326\Research Project - Backup\Assets\Resources", "*.mp3"); // Retrieves files from a specified directory
        musicTitles = new string[music.Length]; // Creating the titles for the menu dropdown
        for (int i = 0; i < music.Length; i++)
        {
            musicTitles[i] = Path.GetFileNameWithoutExtension(music[i]); // Removes the file extension from the titles
        }
        song = (AudioClip)Resources.Load(musicTitles[0]); // Creates an audioclip out of the sound file
        m_MyAudioSource.clip = song; // Sets the audio source clip as the newly created audioclip
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor

        userInterface.GetComponent<Canvas>().enabled = false; // Disables the designated interface

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
        Cursor.visible = true; // Enables the UI and disables the character controls
        Cursor.lockState = CursorLockMode.Confined;
        userInterface.GetComponent<Canvas>().enabled = true;
        rigidBodyFPSController.GetComponent<RigidbodyFirstPersonController>().enabled = false;
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
*/