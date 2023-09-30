using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piranhaController : MonoBehaviour
{
    private bool alert = false;
    private Animator anim;
    private bool podeAtacar = false;
    public playerMove playerMove;
    public float damage;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (alert)
        {
            if (podeAtacar)
            {
                Attack();
                podeAtacar = false;
                playerMove.KBCounter = playerMove.KBTotalTime;
                if (playerMove.transform.position.x <= transform.position.x)
                {
                    playerMove.KnockFromRight = true;
                }
                if (playerMove.transform.position.x > transform.position.x)
                {
                    playerMove.KnockFromRight = false;
                }
                playerMove.currentHealth = playerMove.currentHealth - damage;
                playerMove.healthBar.value = playerMove.currentHealth;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            alert = true;
            if (!podeAtacar)
            {
                podeAtacar = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            alert = false;
        }
    }

    private void Attack()
    {
        anim.SetBool("Attack", true);
    }

    private void ClosePlant()
    {
        anim.SetBool("Open", true);
    }

    private void EndOpen()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Open", false);
    }
}
