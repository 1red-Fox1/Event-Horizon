using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSprites : MonoBehaviour
{
    public Transform player;        

    public SpriteRenderer fadeInImage1;
    public SpriteRenderer fadeInImage2;
    public SpriteRenderer fadeInImage3;
    public float fadeSpeed;
    private float timeTutorial;
    public float timeLimit;
    private bool inRange = false;

    private void Start()
    {
        timeTutorial = 0f;
    }

    void Update()
    {       
        if (inRange)
        {
            timeTutorial += Time.time;
            if (timeTutorial >= timeLimit)
            {
                fadeIn();
            }
        }
    }
    void fadeIn()
    {
        Color imageColor1 = fadeInImage1.color;
        imageColor1.a += fadeSpeed * Time.deltaTime;
        fadeInImage1.color = imageColor1;

        Color imageColor2 = fadeInImage2.color;
        imageColor2.a += fadeSpeed * Time.deltaTime;
        fadeInImage2.color = imageColor2;

        Color imageColor3 = fadeInImage3.color;
        imageColor3.a += fadeSpeed * Time.deltaTime;
        fadeInImage3.color = imageColor3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
}

