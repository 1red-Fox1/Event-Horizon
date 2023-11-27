using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saidasController : MonoBehaviour
{
    public bossBattleTrigger bossBattleTrigger;
    public bossGolemController bossGolemController;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (bossGolemController.inicioBossFight)
        {
            if (bossGolemController.endBossFight)
            {
                Invoke("OpenWall", 6f);
            }
            else
            {
                anim.Play("saidaBossFightClose");
            }
        }
    }
    void OpenWall()
    {
        anim.Play("saidaBossFightOpen");
    }
}
