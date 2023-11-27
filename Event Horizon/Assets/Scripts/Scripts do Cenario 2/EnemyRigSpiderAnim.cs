using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRigSpiderAnim : MonoBehaviour
{    
    public playerMove playerMove;
    private AudioSource audioSource;
    private Animator anim;
    private bool isFlipped = false;
    public bool isAttacking = false;
    public alertColisorRigSpider spiderAlert;
    public float timeCooling;
    public float lasttimecooling;
    private bool cooling = false;
    public bool isInRange = false;
    public int lifeEnemy;
    public bool isDeath = false;
    public int attackCount;
    public AudioClip attackSound;
    public bool death = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        lasttimecooling = timeCooling;
    }

    void Update()
    {
        if (playerMove.transform.position.x <= transform.position.x)
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
        if (spiderAlert.alert && !cooling && !(playerMove.playerAttack && isInRange && playerMove.atacando) && !isDeath)
        {
            Attack();
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (!isAttacking && playerMove.playerAttack && isInRange)
        {
            if (lifeEnemy > 0)
            {
                anim.SetBool("Damaged", true);
                anim.SetBool("Attack", false);
                lifeEnemy--;
            }
            if (lifeEnemy <= 0)
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
        attackCount--;
        if (attackCount <= 0)
        {
            cooling = true;
            anim.SetBool("Attack", false);
        }
    }
    void TriggerCooling()
    {
        attackCount--;
        if(attackCount <= 0)
        {
            cooling = true;
        }
    }
    void CoolDown()
    {
        timeCooling -= Time.deltaTime;
        if (timeCooling <= 0 && cooling)
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

    void returnAttackCount()
    {
        attackCount = 6;
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
        death = true;
        gameObject.SetActive(false);
    }
    void AttackSound()
    {
        audioSource.PlayOneShot(attackSound);
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
}


