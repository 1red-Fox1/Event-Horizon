using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSoundEffects : MonoBehaviour
{
    public float initialVolume;
    public AudioSource audioSource;

    void Start()
    {
        initialVolume = audioSource.volume;
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
    }
    public void ResetVolume()
    {
        audioSource.volume = initialVolume;
    }
}
