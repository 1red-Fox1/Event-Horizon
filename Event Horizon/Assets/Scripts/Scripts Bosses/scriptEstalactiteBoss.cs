using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptEstalactiteBoss : MonoBehaviour
{
    public playerMove playerMove;
    private bool isInRange = false;
    private Vector3 originalPosition;
    public float shakeMagnitude;
    public float shakeDuration;
    public bool attacked = false;
    public int health;
    private Rigidbody2D rb;
    public bool estalactiteDestruida = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        originalPosition = transform.position;
    }
    private void Update()
    {
        if (playerMove.playerAttack && isInRange && health > 0)
        {
            attacked = true;
            ShakeSprite();
            if (attacked)
            {
                health = health - 1;
                attacked = false;
            }
            if (health <= 0)
            {
                rb.isKinematic = false;
            }
        }
    }

    void ShakeSprite()
    {
        StartCoroutine(ShakeCoroutine());
    }

    System.Collections.IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;
        Vector3 startPosition = transform.position;

        while (elapsed < shakeDuration)
        {
            Vector3 randomPosition = startPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.position = randomPosition;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerRange" || collision.gameObject.tag == "mediumplayerRange")
        {
            isInRange = true;
        }
        if(collision.gameObject.tag == "fimEstala")
        {
            estalactiteDestruida = true;
            //gameObject.SetActive(false);
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
