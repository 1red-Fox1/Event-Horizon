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

    private int menuVariable = 2;
    public GameObject menuButton;
    public GameObject menuBG;
    public GameObject menuPanel;
    public GameObject configPanel;
    public GameObject screensPanel;
    private bool podePausar = true;
    public GroundColisor GroundColisor;
    private bool fade = true;

    void Update()
    {
        if (playerMove.isDeath && fade)
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
        screensPanel.SetActive(false);
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
            respawnPlayer();
            Invoke("fadeOut", 1f);
        }
    }
    void fadeOut()
    {
        Color imageColor = fadeInImage.color;
        imageColor.a = 0f;
        fadeInImage.color = imageColor;
    }
    void respawnPlayer()
    {
        int sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if(scene.name == "BossFight1" || scene.name == "Fase2 1" || scene.name == "Fase3 1" || scene.name == "Fase4")
            {
                if (scene.name == "BossFight1")
                {
                    Debug.Log("A cena atual é: " + scene.name);

                    SceneManager.LoadScene("BossFight1");

                    return;
                }
                if (scene.name == "Fase2 1")
                {
                    Debug.Log("A cena atual é: " + scene.name);

                    SceneManager.LoadScene("Fase2 1");

                    return;
                }
                if (scene.name == "Fase3 1")
                {
                    Debug.Log("A cena atual é: " + scene.name);

                    SceneManager.LoadScene("Fase3 1");

                    return;
                }
                if (scene.name == "Fase4")
                {
                    Debug.Log("A cena atual é: " + scene.name);

                    SceneManager.LoadScene("Fase4");

                    return;
                }
            }
            else
            {
                GroundColisor.Respawn();
            }
        }
    }
}
