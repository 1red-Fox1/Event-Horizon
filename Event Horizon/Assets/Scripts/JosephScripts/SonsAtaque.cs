using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonsAtaque : MonoBehaviour
{
    public playerMove playerMove;
    private AudioSource audioSource;
    public AudioClip Atk1;
    public AudioClip Atk2;
    public AudioClip Atk3;
    public AudioClip Grito1;
    public AudioClip Grito2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerMove.atk1)
        {
            audioSource.PlayOneShot(Grito1);
            audioSource.PlayOneShot(Atk1);
            playerMove.atk1 = false;
        }
        if (playerMove.atk2)
        {
            audioSource.PlayOneShot(Atk2);
            playerMove.atk2 = false;
        }
        if (playerMove.atk3)
        {
            audioSource.PlayOneShot(Grito2);
            audioSource.PlayOneShot(Atk3);
            playerMove.atk3 = false;
        }       
    }
}
