using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnKeyPress : MonoBehaviour
{
    // Nome da cena que você deseja carregar
    public string sceneName;

    private void Update()
    {
        // Verifica se a tecla "E" foi pressionada
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Carrega a cena especificada no Inspector
            SceneManager.LoadScene(sceneName);
        }
    }
}




