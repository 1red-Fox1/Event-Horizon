using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estalactitePonta : MonoBehaviour
{
    public colisorEstalactite inRangeColisor;
    public float shakeMagnitude;
    public float shakeDuration;
    public float forcaQueda;

    private Rigidbody2D rb;
    private Vector3 originalPosition;
    public playerMove playerMove;
    private bool isDamaged = false;
    private bool estanoChao = false;
    public SpriteRenderer fadeInImage1;
    public float fadeSpeed;
    private float timeTutorial;
    public float timeLimit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        originalPosition = transform.position;
        timeTutorial = 0f;
    }

    void Update()
    {
        if (inRangeColisor.inRange)
        {
            ShakeSprite();
            inRangeColisor.inRange = false;
        }
        if (estanoChao)
        {
            rb.isKinematic = true;
            timeTutorial += Time.time;
            if (timeTutorial >= timeLimit)
            {
                fadeOut();
            }
        }
        if (isDamaged)
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
    void fadeOut()
    {
        Color imageColor1 = fadeInImage1.color;
        imageColor1.a -= fadeSpeed * Time.deltaTime;
        fadeInImage1.color = imageColor1;

        if(imageColor1.a <= 0)
        {
            Destroy(gameObject);
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
        rb.isKinematic = false;

        Vector3 newPosition = transform.position;
        newPosition.y -= forcaQueda;
        transform.position = newPosition;

        transform.position = originalPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isDamaged = true;
        }
        if(collision.gameObject.tag == "ground")
        {
            estanoChao = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")

        {
            isDamaged = false;
        }
    }
}
