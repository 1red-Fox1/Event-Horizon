 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_Controller : MonoBehaviour
{
    public bool defaultControl = true;
    public bool fullScreen = true;

    void Start()
    {
        if (PlayerPrefs.HasKey("defaultControl"))
        {
            int defaultControlInt = PlayerPrefs.GetInt("defaultControl");
            defaultControl = (defaultControlInt == 1);
        }
        if (PlayerPrefs.HasKey("Screen.fullScreen"))
        {
            int ScreenfullScreenInt = PlayerPrefs.GetInt("Screen.fullScreen");
            Screen.fullScreen = (ScreenfullScreenInt == 1);
        }
        if (PlayerPrefs.HasKey("fullScreen"))
        {
            int fullScreenInt = PlayerPrefs.GetInt("fullScreen");
            fullScreen = (fullScreenInt == 1);
        }
    }

    void Update()
    {
        if (fullScreen)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.SetResolution(960, 540, false);
        }      
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ReturntoMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Paused()
    {
        Time.timeScale = 0f;
    }
    public void Despaused()
    {
        Time.timeScale = 1f;
    }
    public void Controle1Ativo()
    {
        defaultControl = true;
        PlayerPrefs.SetInt("defaultControl", 1);
    }

    public void Controle2Ativo()
    {
        defaultControl = false;
        PlayerPrefs.SetInt("defaultControl", 0);
    }
    public void FullScreenConfigYes()
    {
        Screen.fullScreen = true;
        fullScreen = true;
        PlayerPrefs.SetInt("Screen.fullScreen", 1);
        PlayerPrefs.SetInt("fullScreen", 1);
    }
    public void FullScreenConfigNo()
    {
        Screen.fullScreen = false;
        fullScreen = false;
        PlayerPrefs.SetInt("Screen.fullScreen", 0);
        PlayerPrefs.SetInt("fullScreen", 0);
    }

    public void GoToFase1()
    {
        SceneManager.LoadScene("Fase1");
    }
    public void GoToFase2()
    {
        SceneManager.LoadScene("Fase2.0");
    }
    public void GoToFase3()
    {
        SceneManager.LoadScene("Fase3");
    }
    public void GoToFase4()
    {
        SceneManager.LoadScene("Fase4");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void GoToFurao()
    {
        SceneManager.LoadScene("BossFight1");
    }
}
