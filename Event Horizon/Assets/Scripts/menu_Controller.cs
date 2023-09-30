 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_Controller : MonoBehaviour
{
    public bool defaultControl = true;
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
    }
    public void Controle2Ativo()
    {
        defaultControl = false;
    }
}
