using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objetoQuebravel : MonoBehaviour
{
    public playerMove playerMove;
    private bool isInRange = false;
    private Vector3 originalPosition;
    public float shakeMagnitude;
    public float shakeDuration;
    public bool attacked = false;
    public int health;

    void Start()
    {
        originalPosition = transform.position;
    }
    private void Update()
    {
        if(playerMove.playerAttack && isInRange)
        {
            attacked = true;
            ShakeSprite();
            if (attacked)
            {
                health = health - 1;
                attacked = false;
            }            
            if(health <= 0)
            {
                Destroy(gameObject);
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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerRange" || collision.gameObject.tag == "mediumplayerRange")
        {
            isInRange = false;
        }
    }
}
