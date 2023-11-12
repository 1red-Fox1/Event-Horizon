using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cupimController : MonoBehaviour
{
    public playerMove playerMove;
    private bool attackRange;
    public float moveSpeed;
    public Transform player;
    private Rigidbody2D rb;
    public float maxDistance;
    private bool podeMover = false;
    public bool encostou = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);

        if (distanceX < maxDistance)
        {
            podeMover = true;
        }

        if (podeMover)
        {
            if (playerMove.transform.position.x < transform.position.x)
            {
                Vector2 moveDirection = new Vector2(1f, 0f);
                rb.velocity = moveDirection * moveSpeed;
            }
            if (playerMove.transform.position.x > transform.position.x)
            {
                Vector2 moveDirection = new Vector2(-1f, 0f);
                rb.velocity = moveDirection * moveSpeed;
            }          
        }

        if (rb.velocity.x > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (rb.velocity.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (attackRange && !playerMove.isDefending && !playerMove.playerAttack && !playerMove.atacando)
        {
            playerMove.KBCounter = playerMove.KBTotalTime;
            if (playerMove.transform.position.x <= transform.position.x)
            {
                playerMove.KnockFromRight = true;
            }
            if (playerMove.transform.position.x > transform.position.x)
            {
                playerMove.KnockFromRight = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackRange = true;
        }
        if(collision.gameObject.tag == "MainCamera" && !encostou)
        {
            encostou = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackRange = false;
        }
        if(collision.gameObject.tag  == "MainCamera" && encostou)
        {
            encostou = false;
            Destroy(gameObject);
        }
    }
}
