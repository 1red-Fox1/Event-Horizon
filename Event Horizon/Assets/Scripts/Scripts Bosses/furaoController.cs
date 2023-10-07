using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furaoController : MonoBehaviour
{
    private Animator anim;
    public Transform[] moveSpots;
    public float speed;
    private float waitTime;
    public float startWaitTime;
    private int currentSpot = 0;
    private Vector2 previousPosition;
    private bool facingRight;
    public GameObject player;

    private void Start()
    {
        waitTime = startWaitTime;
        currentSpot = 0;
        previousPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Damaged", false);

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
        }
        else
        {
            anim.SetBool("Walk", false);

            // Virar para o jogador quando parado
            if (player != null)
            {
                if (transform.position.x > player.transform.position.x && !facingRight)
                {
                    Flip();
                }
                else if (transform.position.x < player.transform.position.x && facingRight)
                {
                    Flip();
                }
            }
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

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
