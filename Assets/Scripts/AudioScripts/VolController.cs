using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolController : MonoBehaviour
{
    public AudioMixer mixer;
    string audioGroup;
    public void SetAudioGroup(string input)
    {
        audioGroup = input; //ambienceVol, characterVol, masterVol, missionControlVol, musicVol, UIVol, environmentVol
    }

    public void SetSound(float soundLevel)
    {
        mixer.SetFloat(audioGroup, soundLevel);
    }
}