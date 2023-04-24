using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbienceVolController : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetSound(float soundLevel)
    {
        mixer.SetFloat("ambienceVol", soundLevel);
    }
}
