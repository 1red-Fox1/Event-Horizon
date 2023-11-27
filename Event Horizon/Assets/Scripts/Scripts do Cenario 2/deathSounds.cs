using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathSounds : MonoBehaviour
{
    public EnemyRigSpiderAnim spider1;
    public EnemyRigSpiderAnim spider2;
    public EnemyRigSpiderAnim spider3;
    public EnemyRigSpiderAnim spider4;
    public EnemyRigSpiderAnim spider5;
    public EnemyRigSpiderAnim spider6;
    private AudioSource audioSource;
    public AudioClip morteAranha;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (spider1.death)
        {
            spider1.death = false;
            audioSource.PlayOneShot(morteAranha);
        }
        if (spider2.death)
        {
            spider2.death = false;
            audioSource.PlayOneShot(morteAranha);
        }
        if (spider3.death)
        {
            spider3.death = false;
            audioSource.PlayOneShot(morteAranha);
        }
        if (spider4.death)
        {
            spider4.death = false;
            audioSource.PlayOneShot(morteAranha);
        }
        if (spider5.death)
        {
            spider5.death = false;
            audioSource.PlayOneShot(morteAranha);
        }
        if (spider6.death)
        {
            spider6.death = false;
            audioSource.PlayOneShot(morteAranha);
        }
    }
}
