using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTronco : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip[] ataqueInimigo;

    #region Variaveis de Patrulha
    public Transform[] moveSpots;
    public float speed;
    private float waitTime;
    public float startWaitTime;
    private int randomSpot;
    private Vector2 previousPosition;
    private bool facingRight;
    #endregion

    public playerMove playerMove;
    public colisorEnemy colisorEnemy;
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
    public bool morteTronco;
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        previousPosition = transform.position;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();       
    }

    private void Update()
    {
        if (colisorEnemy.alert)
        {
            Attack();
        }
        if(!colisorEnemy.alert && !isDeath && podeMover)
        {
            Patrol();
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
            if(lifeEnemy > 0)
            {
                podeMover = false;
                anim.SetBool("Damaged", true);
                isEnemyKnockbackDamage = true;
                enemyKnockbackStartPositionDamage = transform.position;
                enemyKnockbackTimerDamage = enemyKnockbackDurationDamage;
                lifeEnemy--;
            }
            if(lifeEnemy <= 0)
            {
                anim.SetBool("Death", true);
                isEnemyKnockbackDamage = true;
                enemyKnockbackStartPositionDamage = transform.position;
                enemyKnockbackTimerDamage = enemyKnockbackDurationDamage;
                isDeath = true;
            }           
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

        if (transform.position.x < previousPosition.x && !facingRight)
        {
            Flip();
            facingRight = true;
        }
        else if (transform.position.x > previousPosition.x && facingRight)
        {
            Flip();
            facingRight = false;
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
    }
    #endregion

    void StopAttack()
    {
        anim.SetBool("Attack", false);
    }    
    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
        morteTronco = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "playerRange" || collision.gameObject.tag == "mediumplayerRange")
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


