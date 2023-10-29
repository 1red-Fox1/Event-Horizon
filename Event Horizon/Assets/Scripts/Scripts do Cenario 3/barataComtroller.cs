using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class barataComtroller : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    public Vector2 jumpDirectionRight = new Vector2(1f, 4f);
    public Vector2 jumpDirectionLeft = new Vector2(-1f, 4f);
    private Vector2 previousPosition;

    private Animator anim;
    private AudioSource audioSource;
    public AudioClip[] ataqueInimigo;

    #region Variaveis de Patrulha
    public Transform[] moveSpots;
    public float speed;
    private float waitTime;
    public float startWaitTime;
    private int randomSpot;
    #endregion

    public playerMove playerMove;
    public colisorEnemyBarata colisorEnemy;
    public bool isEnemyKnockback = false;
    private Vector3 enemyKnockbackStartPosition;
    private float enemyKnockbackTimer;
    public float enemyKnockbackDuration;
    public float enemyKnockbackDistanceX;
    public bool isAttack = false;

    public bool isEnemyKnockbackDamage = false;
    private Vector3 enemyKnockbackStartPositionDamage;
    private float enemyKnockbackTimerDamage;
    public float enemyKnockbackDurationDamage;
    public float enemyKnockbackDistanceXDamage;
    public bool isInRange = false;
    public int lifeEnemy;
    public bool isDeath = false;
    public bool podeMover = true;
    public bool Passo = false;
    public bool morteBarata;
    public bool attackLeft = false;
    public bool attackRight = false;
    public sapoAlert barataAlert;
    public bool isGrounded;
    private int attackCount;
    public float attackTimer;
    public float attackTiming;
    public limitePulo lim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        previousPosition = transform.position;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isGrounded)
        {
            attackCount = 1;
            if (barataAlert.alert)
            {
                if (attackTimer >= attackTiming)
                {
                    Attack();
                    attackTimer = 0f;
                }
            }
            if (!barataAlert.alert && !isDeath && podeMover)
            {
                Patrol();
            }
            attackTimer += Time.deltaTime;
        }
        else
        {
            attackCount = 0;
        }

        if (isEnemyKnockback)
        {
            if (enemyKnockbackTimer > 0)
            {
                Vector3 knockbackMovement = new Vector3((transform.position.x - playerMove.transform.position.x) * enemyKnockbackDistanceX, 0, 0);
                transform.position += knockbackMovement * Time.deltaTime;

                enemyKnockbackTimer -= Time.deltaTime;
            }
            else
            {
                isEnemyKnockback = false;
            }
        }

        if (isEnemyKnockbackDamage)
        {
            if (enemyKnockbackTimerDamage > 0)
            {
                Vector3 knockbackMovement = new Vector3((transform.position.x - playerMove.transform.position.x) * enemyKnockbackDistanceXDamage, 0, 0);
                transform.position += knockbackMovement * Time.deltaTime;

                enemyKnockbackTimerDamage -= Time.deltaTime;
            }
            else
            {
                isEnemyKnockbackDamage = false;
            }
        }

        if (!isAttack && playerMove.playerAttack && isInRange)
        {
            if (lifeEnemy > 0)
            {
                podeMover = false;
                anim.SetBool("Damaged", true);
                isEnemyKnockbackDamage = true;
                enemyKnockbackStartPositionDamage = transform.position;
                enemyKnockbackTimerDamage = enemyKnockbackDurationDamage;
                lifeEnemy--;
            }
            if (lifeEnemy <= 0)
            {
                anim.SetBool("Death", true);
                isEnemyKnockbackDamage = true;
                enemyKnockbackStartPositionDamage = transform.position;
                enemyKnockbackTimerDamage = enemyKnockbackDurationDamage;
                isDeath = true;
            }
        }
    }
    void FixedUpdate()
    {
        if (attackLeft && attackCount > 0)
        {
            Vector2 jumpVector = jumpDirectionRight.normalized * jumpForce;
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
            UnFlip();
            attackLeft = false;
        }
        if (attackRight && attackCount > 0)
        {
            Vector2 jumpVector = jumpDirectionLeft.normalized * jumpForce;
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
            Flip();
            attackRight = false;
        }
    }
    #region Patrulha do inimigo
    void Patrol()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Damaged", false);

        previousPosition = transform.position;

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
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
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (transform.position.x > previousPosition.x)
        {
            Flip();
        }
        else if (transform.position.x < previousPosition.x)
        {
            UnFlip();
        }
    }
    #endregion

    #region Ataque do inimigo
    void Attack()
    {
        anim.SetBool("Attack", true);
        anim.SetBool("Walk", false);
        anim.SetBool("Damaged", false);

        if (playerMove.isDefending && isAttack)
        {
            isEnemyKnockback = true;
            enemyKnockbackStartPosition = transform.position;
            enemyKnockbackTimer = enemyKnockbackDuration;
        }
        if(playerMove.transform.position.x < transform.position.x && !attackLeft)
        {
            attackLeft = true;
            attackRight = false;
        }
        if (playerMove.transform.position.x > transform.position.x && !attackRight)
        {
            attackLeft = false;
            attackRight = true;
        }
    }
    #endregion

    void StopAttack()
    {
        anim.SetBool("Attack", false);
    }

    void Flip()
    {
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    void UnFlip()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    private void SomDeAtaque()
    {
        audioSource.PlayOneShot(ataqueInimigo[Random.Range(0, ataqueInimigo.Length)]);
    }

    private void Passos()
    {
        Passo = true;
    }
    private void isAttacking()
    {
        isAttack = true;
    }
    private void isnotAttacking()
    {
        isAttack = false;
    }
    private void endDamage()
    {
        podeMover = true;
        anim.SetBool("Damaged", false);
    }
    private void endLife()
    {
        Destroy(gameObject);
    }
    void DeathSound()
    {
        morteBarata = true;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
}
