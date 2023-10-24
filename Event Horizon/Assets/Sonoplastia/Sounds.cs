using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public EnemyTronco tronco;
    public aranhaCanhaoAnim aranhaCanhao;
    public RatoController rato;

    public EnemyTronco tronco2;
    public aranhaCanhaoAnim aranhaCanhao2;
    public RatoController rato2;

    public EnemyTronco tronco3;
    public aranhaCanhaoAnim aranhaCanhao3;
    public RatoController rato3;

    public aranhaCanhaoAnim aranhaCanhao4;

    private AudioSource audioSource;
    public AudioClip morteTronco;
    public AudioClip morteAranhaCanhao;
    public AudioClip morteRato;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (rato.morteRato)
        {
            audioSource.PlayOneShot(morteRato);
            rato.morteRato = false;
        }
        if (aranhaCanhao.morteAranhaCanhao)
        {
            audioSource.PlayOneShot(morteAranhaCanhao);
            aranhaCanhao.morteAranhaCanhao = false;
        }
        if (tronco.morteTronco)
        {
            audioSource.PlayOneShot(morteTronco);
            tronco.morteTronco = false;
        }

        //2
        if (rato2.morteRato)
        {
            audioSource.PlayOneShot(morteRato);
            rato2.morteRato = false;
        }
        if (aranhaCanhao2.morteAranhaCanhao)
        {
            audioSource.PlayOneShot(morteAranhaCanhao);
            aranhaCanhao2.morteAranhaCanhao = false;
        }
        if (tronco2.morteTronco)
        {
            audioSource.PlayOneShot(morteTronco);
            tronco2.morteTronco = false;
        }

        //3
        if (rato3.morteRato)
        {
            audioSource.PlayOneShot(morteRato);
            rato3.morteRato = false;
        }
        if (aranhaCanhao3.morteAranhaCanhao)
        {
            audioSource.PlayOneShot(morteAranhaCanhao);
            aranhaCanhao3.morteAranhaCanhao = false;
        }
        if (tronco3.morteTronco)
        {
            audioSource.PlayOneShot(morteTronco);
            tronco3.morteTronco = false;
        }

        //4
        if (aranhaCanhao4.morteAranhaCanhao)
        {
            audioSource.PlayOneShot(morteAranhaCanhao);
            aranhaCanhao4.morteAranhaCanhao = false;
        }
    }
}
