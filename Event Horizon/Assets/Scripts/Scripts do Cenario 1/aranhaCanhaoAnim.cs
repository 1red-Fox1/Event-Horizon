using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aranhaCanhaoAnim : MonoBehaviour
{
    private Animator anim;
    private bool isFlipped = false;
    public playerMove playerMove;
    public bool isAttacking = false;
    public spiderAlertColisor spiderAlert;
    public float timeCooling;
    public float lasttimecooling;
    private bool cooling = false;
    public bool isInRange = false;
    public int lifeEnemy;
    public bool isDeath = false;
    private AudioSource audioSource;
    public AudioClip attackSound;
    public bool morteAranhaCanhao;

    private void Start()
    {
        anim = GetComponent<Animator>();
        lasttimecooling = timeCooling;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(playerMove.transform.position.x <= transform.position.x)
        {
            Unflip();
        }
        if (playerMove.transform.position.x > transform.position.x)
        {
            Flip();
        }
        if (cooling)
        {
            CoolDown();
            anim.SetBool("Attack", false);
        }
        if (spiderAlert.spiderAlert && !cooling && !(playerMove.playerAttack && isInRange && playerMove.atacando) && !isDeath)
        {
            Attack();
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (!isAttacking && playerMove.playerAttack && isInRange)
        {
            if(lifeEnemy > 0)
            {
                anim.SetBool("Damaged", true);
                anim.SetBool("Attack", false);
                lifeEnemy--;
            }    
            if(lifeEnemy <= 0)
            {
                anim.SetBool("Death", true);
                isDeath = true;
            }
        }
    }

    void Attack()
    {
        timeCooling = lasttimecooling;

        anim.SetBool("Attack", true);
    }
    void StopAttack()
    {
        cooling = true;
        anim.SetBool("Attack", false);
    }
    void TriggerCooling()
    {
        cooling = true;
    }
    void CoolDown()
    {
        timeCooling -= Time.deltaTime;
        if(timeCooling <= 0 && cooling)
        {
            cooling = false;
            timeCooling = lasttimecooling;
        }
    }

    void Flip()
    {
        if (!isFlipped)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    void Unflip()
    {
        if (isFlipped)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isFlipped = false;
        }
    }

    void onAttack()
    {
        isAttacking = true;
    }
    void notAttack()
    {
        isAttacking = false;
    }
    void stopDamage()
    {
        anim.SetBool("Damaged", false);
    }
    void endLife()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerRange" || collision.gameObject.tag == "mediumplayerRange")
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerRange" || collision.gameObject.tag == "mediumplayerRange")
        {
            isInRange = false;
        }
    }

    void AttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    void DeathSound()
    {
        morteAranhaCanhao = true;
    }
}
