using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadeController : MonoBehaviour
{
    private Animator anim;
    public GroundColisor player;
    public int sceneId;

    public float loadingSpeed;
    private bool isLoading = false;
    private int sceneToLoad = -1;
    public GameObject text;
    public GameObject LoadingScreen;
    public Slider LoadingBar;

    void Start()
    {
        anim = GetComponent<Animator>();
        LoadingBar.value = 0.0f;
    }

    private void Update()
    {
        if (player.fade1)
        {
            anim.SetBool("Fade", true);
            DisableAudio();
        }
        if (isLoading && LoadingBar.value >= 1.0f && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneToLoad));
        }
        if (LoadingBar.value >= 1.0f)
        {
            text.SetActive(true);
        }
    }
    void DisableAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.enabled = false;
        }
    }
    void FimFade()
    {
        LoadScene(sceneId);
    }
    public void LoadScene(int sceneId)
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
