using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPasso1 : MonoBehaviour
{
    public playerMove playerMove;
    private AudioSource audioSource;
    public AudioClip[] passoGrama;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (playerMove.passoGrama1)
        {
            audioSource.PlayOneShot(passoGrama[Random.Range(0, passoGrama.Length)]);
            playerMove.passoGrama1 = false;
        }        
    }
}
