using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource jumpingSound;
    public AudioSource runningSound;
    public AudioSource runningSound2;
    public AudioSource landingSound;
    public AudioSource closeToLeftArmsSound;
    public AudioSource touchByArmsSound;
    public AudioSource doorShutSound;
    public AudioSource onAirSound;
    public AudioSource deepSighSound;

    public void PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
    }
    public void PlaySound(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioClip == null) return;

        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }
    public void MuteSound(AudioSource audioSource, bool mute)
    {
        Debug.Log($"Muting {audioSource.name}: {mute}");
        if (audioSource != null)
        {
            audioSource.mute = mute;
        }
    }
}

[Serializable]
public class Sound
{
    public AudioClip audioClip;
    public bool isPlaying;
}