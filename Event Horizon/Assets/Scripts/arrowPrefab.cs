using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowPrefab : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasHitGround;
    public bool estanoChao = false;
    public SpriteRenderer fadeInImage1;
    public float fadeSpeed;
    private float timeTutorial;
    public float timeLimit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeTutorial = 0f;
    }

    void Update()
    {
        if (!hasHitGround)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (estanoChao)
        {            
            timeTutorial += Time.time;
            if (timeTutorial >= timeLimit)
            {
                fadeOut();
            }
        }
    }
    void fadeOut()
    {
        Color imageColor1 = fadeInImage1.color;
        imageColor1.a -= fadeSpeed * Time.deltaTime;
        fadeInImage1.color = imageColor1;

        if (imageColor1.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Grama")
        {
            hasHitGround = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            estanoChao = true;
        }
    }
}
