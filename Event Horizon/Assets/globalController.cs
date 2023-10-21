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

    private int menuVariable = 0;
    public GameObject menuButton;
    public GameObject menuBG;
    public GameObject menuPanel;
    public GameObject configPanel;
    private bool podePausar = true;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuVariable = menuVariable + 1;
        }
        if(menuVariable == 1)
        {
            if (podePausar)
            {
                OpenMenu();
            }
        }
        if(menuVariable == 2)
        {
            if (!podePausar)
            {
                CloseMenu();
            }
        }
        if(menuVariable > 2)
        {
            menuVariable = 1;
        }
    }
    void OpenMenu()
    {
        menuButton.SetActive(false);
        menuBG.SetActive(true);
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
        podePausar = false;
    }
    void CloseMenu()
    {
        menuButton.SetActive(true);
        menuBG.SetActive(false);
        menuPanel.SetActive(false);
        configPanel.SetActive(false);
        Time.timeScale = 1f;
        podePausar = true;
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
