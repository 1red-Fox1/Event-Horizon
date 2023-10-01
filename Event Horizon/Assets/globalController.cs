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

    public GameObject Ok1;
    public GameObject Ok2;
    public GameObject FullScreenYes;
    public GameObject FullScreenNo;
    public menu_Controller options;


    void Update()
    {
        if (playerMove.isDeath)
        {
            fadeIn();
        }

        if (options.defaultControl)
        {
            Ok1.SetActive(true);
            Ok2.SetActive(false);
        }
        else
        {
            Ok1.SetActive(false);
            Ok2.SetActive(true);
        }

        if (options.fullScreen)
        {
            FullScreenYes.SetActive(true);
            FullScreenNo.SetActive(false);
        }
        else
        {
            FullScreenYes.SetActive(false);
            FullScreenNo.SetActive(true);
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
