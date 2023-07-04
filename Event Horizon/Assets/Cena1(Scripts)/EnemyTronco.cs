using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTronco : MonoBehaviour
{
    private Animator anim;

    #region Variaveis de Patrulha
    public Transform[] moveSpots;
    public float speed;
    private float waitTime;
    public float startWaitTime;
    private int randomSpot;
    private Vector2 previousPosition;
    private bool facingRight;
    #endregion

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        previousPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Patrol();
    }

    void Patrol()
    {
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

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}


