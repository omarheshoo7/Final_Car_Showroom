using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineController : MonoBehaviour
{
    public AudioSource engineAudioSource;
    public AudioClip engineStartClip;

    private bool isEngineOn = false;

    public void ToggleEngine()
    {
        if (!isEngineOn)
        {
            engineAudioSource.clip = engineStartClip;
            engineAudioSource.Play();
            isEngineOn = true;
        }
        else
        {
            engineAudioSource.Stop();
            isEngineOn = false;
        }
    }
}
