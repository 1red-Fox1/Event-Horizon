using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saidasController : MonoBehaviour
{
    public bossBattleTrigger bossBattleTrigger;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (bossBattleTrigger.bossCamera)
        {
            anim.Play("saidaBossFightClose");
        }
        else
        {
            anim.Play("saidaBossFightOpen");
        }
    }
}
