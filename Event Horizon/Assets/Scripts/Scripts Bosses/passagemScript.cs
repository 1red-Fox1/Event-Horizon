using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passagemScript : MonoBehaviour
{
    public furaoController1 inRangeColisor;
    public float shakeMagnitude;
    public float shakeDuration;
    public float quedaSpeed; 
    private Rigidbody2D rb;
    private Vector3 originalPosition;

    private bool caindo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        originalPosition = transform.position;
    }

    void Update()
    {
        if (inRangeColisor.healthAmount <= 0)
        {
            if (!caindo)
            {
                ShakeSprite();
            }
        }

        if (caindo)
        {
            MoverParaBaixo();
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

        caindo = true;
        rb.isKinematic = true;
    }

    void MoverParaBaixo()
    {
        Vector3 newPosition = transform.position;
        newPosition.y -= quedaSpeed * Time.deltaTime; 
        transform.position = newPosition;
    }
}
