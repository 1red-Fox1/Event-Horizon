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

    #region Variaveis de Ataque
    private bool alert = false;
    private float timerDuration = 2f;
    #endregion

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
        if (alert)
        {
            Attack();
        }
        if (!alert)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        anim.SetBool("Attack", false);

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

    void Attack()
    {
        //isAttaking = true;
        //if (isAttaking)
        //{
        //    isAttaking = false;
        //    anim.SetBool("Attack", true);
        //    StartCoroutine(StartTimer());
        //    isAttaking = true;
        //}
        if (alert)
        {
            anim.SetBool("Attack", true);
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Walk", true);
        }
                
    }

    private IEnumerator StartTimer()
    {
        anim.SetBool("Attack", false);
        yield return new WaitForSeconds(timerDuration);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            alert = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            alert =  false;
        }
    }

    private void SomDeAtaque()
    {
        audioSource.PlayOneShot(ataqueInimigo[Random.Range(0, ataqueInimigo.Length)]);        
    }
}


