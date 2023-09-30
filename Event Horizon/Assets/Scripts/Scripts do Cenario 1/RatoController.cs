using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatoController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip ataqueInimigo;
    public AudioClip deathSound;

    private Animator anim;
    public playerMove playerMove;
    public attackRangeRato isattackRange;
    public ColisorRato colisorRato;
    public Transform target;
    public float followSpeed;
    public Transform[] moveSpots;
    public float speed;
    private float waitTime;
    public float startWaitTime;
    private int randomSpot;
    private Vector2 previousPosition;
    private bool facingRight;

    public bool isEnemyKnockback = false;
    private Vector3 enemyKnockbackStartPosition;
    private float enemyKnockbackTimer;
    public float enemyKnockbackDuration;
    public float enemyKnockbackDistanceX;
    public bool isAttack = false;
    public bool isInRange = false;

    public bool isEnemyKnockbackDamage = false;
    private Vector3 enemyKnockbackStartPositionDamage;
    private float enemyKnockbackTimerDamage;
    public float enemyKnockbackDurationDamage;
    public float enemyKnockbackDistanceXDamage;
    public int lifeEnemy;
    public bool isDeath = false;
    public bool podeMover = true;
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        previousPosition = transform.position;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!colisorRato.Alert && !isDeath)
        {
            if (podeMover)
            {
                Patrol();
            }
        }
        else
        {
            if (isattackRange.attackRange)
            {
                Attack();
            }
            if(colisorRato.Alert && !isattackRange.attackRange && podeMover)
            {
                Run();
            }
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

    void Run()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Run", true);

        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    void Attack()
    {
        anim.SetBool("Attack", true);
        anim.SetBool("Run", false);

        if (playerMove.isDefending && isAttack)
        {
            isEnemyKnockback = true;
            enemyKnockbackStartPosition = transform.position;
            enemyKnockbackTimer = enemyKnockbackDuration;
        }
    }

    void Patrol()
    {
        anim.SetBool("Attack", false);

        Vector2 movementDirection = (Vector2)transform.position - (Vector2)previousPosition;
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

        if (movementDirection.x != 0)
        {
            anim.SetBool("Run", true);
            if (movementDirection.x > 0 && !facingRight)
            {
                Flip();
                facingRight = true;
            }
            else if (movementDirection.x < 0 && facingRight)
            {
                Flip();
                facingRight = false;
            }
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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

    private void SomDeAtaque()
    {
        audioSource.PlayOneShot(ataqueInimigo);
    }

    private void DeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }
}
