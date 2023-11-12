using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rageController : MonoBehaviour
{
    public furaoController1 furao;
    public attackRain attack;
    private Color novaCor = new Color(0xDB / 255.0f, 0xA1 / 255.0f, 0xA1 / 255.0f, 1.0f);


    void Update()
    {
        if(furao.healthAmount <= 2)
        {
            Rage();
        }
    }
    void Rage()
    {
        furao.anim.speed = 1.5f;
        furao.speed = 4f;
        furao.startWaitTime = 2f;
        furao.spriteRenderer.color = novaCor;
        attack.shootForce = 5f;
        attack.shootInterval = 0.3f;

    }
}
