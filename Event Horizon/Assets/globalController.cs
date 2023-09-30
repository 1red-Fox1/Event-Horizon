using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class globalController : MonoBehaviour
{
    public Image fadeInImage;
    public float fadeSpeed;
    public playerMove playerMove;
    public string sceneName;

    public SpriteRenderer bg1;
    public SpriteRenderer bg2;
    public SpriteRenderer bg3;
    private Color corOriginalBg1;
    private Color corOriginalBg2;
    private Color corOriginalBg3;

    void Start()
    {
        corOriginalBg1 = bg1.color;
        corOriginalBg2 = bg2.color;
        corOriginalBg3 = bg3.color;
    }

    void Update()
    {
        if (playerMove.isDeath)
        {
            fadeIn();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            corOriginalBg1.a = 1.0f;
            corOriginalBg2.a = 1.0f;
            corOriginalBg3.a = 1.0f;
            bg1.color = corOriginalBg1;
            bg2.color = corOriginalBg2;
            bg3.color = corOriginalBg3;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            corOriginalBg1.a = 0.0f;
            corOriginalBg2.a = 0.0f;
            corOriginalBg3.a = 0.0f;
            bg1.color = corOriginalBg1;
            bg2.color = corOriginalBg2;
            bg3.color = corOriginalBg3;
        }
    }
    void fadeIn()
    {
        Color imageColor = fadeInImage.color;
        imageColor.a += fadeSpeed * Time.deltaTime;
        fadeInImage.color = imageColor;
        if (imageColor.a >= 1.0f)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
