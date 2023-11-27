using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furaoController1 : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Transform[] moveSpots;
    public float speed;
    private float waitTime;
    public float startWaitTime;
    private int currentSpot = 0;
    private Vector2 previousPosition;
    private bool facingRight;
    public GameObject player;
    public int attackCount;
    public bool isAttacking = false;
    public bool fliped;
    private bool Damaged = false;
    public int healthAmount;
    private int times;
    private AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip death;
    public bool rugido = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        waitTime = startWaitTime;
        currentSpot = 0;
        previousPosition = transform.position;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rugido = true;
        Flip();
    }

    private void Update()
    {
        if (rugido)
        {
            anim.SetBool("Rugido", true);
        }
        else
        {
            if (!Damaged)
            {
                times = 1;
                Patrol();
            }
            else if (times == 1)
            {
                times = 0;
                IsDamaged();
            }
        }
    }

    private void Patrol()
    {
        previousPosition = transform.position;

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[currentSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[currentSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                currentSpot = (currentSpot + 1) % moveSpots.Length;
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (transform.position.x != previousPosition.x)
        {
            anim.SetBool("Walk", true);
            attackCount = 3;
        }
        else
        {
            anim.SetBool("Walk", false);
            Invoke("Attack", 0.5f);

            if (player != null)
            {
                if (transform.position.x > player.transform.position.x)
                {
                    Invoke("Flip", 0.15f);
                }
                else if (transform.position.x < player.transform.position.x)
                {
                    Invoke("UnFlip", 0.15f);
                }
            }
        }

        if (transform.position.x < previousPosition.x)
        {
            Flip();
        }
        else if (transform.position.x > previousPosition.x)
        {
            UnFlip();
        }
    }

    void Attack()
    {
        if(transform.position.x == previousPosition.x && attackCount > 0)
        {
            anim.SetBool("Attack", true);
        }
        else if (attackCount <= 0)
        {
            anim.SetBool("Attack", false);
        }
    }

    void IsDamaged()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Damaged", true);

        healthAmount = healthAmount - 1;

        if(healthAmount <= 0)
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Damaged", false);
            anim.SetBool("Death", true);
            Invoke("DestroyBoss", 2f);
        }
    }

    void AttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }
    void DeathSound()
    {
        audioSource.PlayOneShot(death);
    }
    void DestroyBoss()
    {
        gameObject.SetActive(false);
    }
    void Times()
    {
        times = 1;
        Damaged = false;
    }
    void EndDamage()
    {
        anim.SetBool("Damaged", false);
        Damaged = false;
    }

    void AttackCount()
    {
        attackCount = attackCount - 1;
    }

    void onAttack()
    {
        isAttacking = true;
    }
    void notAttack()
    {
        isAttacking = false;
    }
    void EndRugido()
    {
        rugido = false;
        anim.SetBool("Rugido", false);
    }
    void Flip()
    {
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        fliped = true;
    }
    void UnFlip()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        fliped = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            Damaged = true;
        }
    }
}
