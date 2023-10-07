using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutSceneScript : MonoBehaviour
{
    public int sceneId;

    void Start()
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(9.55f);
        SceneManager.LoadScene(sceneId);
    }
}
