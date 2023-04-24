using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterVolControl : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetSound(float soundLevel)
    {
        mixer.SetFloat("masterVol", soundLevel);
    }
}
