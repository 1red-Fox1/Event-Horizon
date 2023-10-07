using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingController : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingBar;
    public float loadingSpeed;

    private bool isLoading = false;
    private int sceneToLoad = -1;
    public GameObject text;

    private void Start()
    {
        LoadingBar.value = 0.0f;
    }

    private void Update()
    {
        if (isLoading && LoadingBar.value >= 1.0f && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneToLoad));
        }
        if(LoadingBar.value >= 1.0f)
        {
            text.SetActive(true);
        }
    }

    public void StartLoading(int sceneId)
    {
        if (!isLoading)
        {
            sceneToLoad = sceneId;
            isLoading = true;
            LoadingScreen.SetActive(true);
            StartCoroutine(IncreaseLoadingBar());
        }
    }

    IEnumerator IncreaseLoadingBar()
    {
        float progress = 0.0f;
        while (progress < 1.0f)
        {
            progress += Time.deltaTime * loadingSpeed;
            LoadingBar.value = progress;
            yield return null;
        }
    }

    IEnumerator LoadSceneAsyncCoroutine(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}

