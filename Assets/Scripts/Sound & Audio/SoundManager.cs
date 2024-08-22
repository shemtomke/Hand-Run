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
    public AudioSource doorCreakSound;
    public AudioSource doorShutSound;
    public AudioSource onAirSound;
    public AudioSource deepSighSound;

    [SerializeField] GameManager gameManager;
    void Start()
    {
        StartCoroutine(PlayDoorCreakAtIntervals());
    }

    private IEnumerator PlayDoorCreakAtIntervals()
    {
        while (gameManager.IsStartGame() && !(gameManager.IsGameOver() || gameManager.IsGameWin() || gameManager.IsPause())) // Loop indefinitely
        {
            // Randomly select an interval time (30, 45, or 60 seconds)
            float interval = UnityEngine.Random.Range(0, 3);
            float waitTime;

            if (interval == 0)
                waitTime = 30f;
            else if (interval == 1)
                waitTime = 45f;
            else
                waitTime = 60f;

            yield return new WaitForSeconds(waitTime);

            // Play the door creaking sound
            if (doorCreakSound != null)
            {
                doorCreakSound.Play();
            }
        }
    }
    public void PlaySound(AudioSource audioSource)
    {
        if (audioSource.isPlaying) return;

        audioSource.Play();
    }
    public void PlaySound(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioClip == null) return;
        if (audioSource.isPlaying) return;

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
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.mute = mute;
        }
        Debug.Log($"Muting {audioSource.name}: {mute}");
    }
    public void PauseSound(AudioSource audioSource, bool pause)
    {
        if (audioSource != null)
        {
            if(pause)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.UnPause();
            }
        }
    }
}

[Serializable]
public class Sound
{
    public AudioClip audioClip;
    public bool isPlaying;
}