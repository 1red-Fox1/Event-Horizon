using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassosInimigoTronco : MonoBehaviour
{
    public EnemyTronco enemy;
    private AudioSource audioSource;
    public AudioClip[] Passos;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (enemy.Passo)
        {
            audioSource.PlayOneShot(Passos[Random.Range(0, Passos.Length)]);
            enemy.Passo = false;
        }
    }
}
