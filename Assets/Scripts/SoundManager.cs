using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource jumpingSound;
    public AudioSource runningSound;
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
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }
    
}
