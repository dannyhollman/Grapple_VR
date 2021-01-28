using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioClip takingDamage;
    public AudioClip dying;

    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamageAudio()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = takingDamage;
        audioSource.Play();
    }
}
