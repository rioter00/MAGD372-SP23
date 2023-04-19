using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CharacterVolController : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetSound(float soundLevel)
    {
        mixer.SetFloat("characterVol", soundLevel);
    }
}
