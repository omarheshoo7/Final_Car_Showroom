using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceoverPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip voiceoverClip;

    public void PlayVoiceover()
    {
        if (!audioSource.isPlaying && voiceoverClip != null)
        {
            audioSource.PlayOneShot(voiceoverClip);
        }
    }
}
