using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GroundColisor : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingBar;
    public playerMove playerMove;
    public int sceneId;
    private AudioSource audioSource;
    public menu_Controller control;
    [SerializeField] private AudioClip landingSound;

    public float loadingSpeed;
    private bool isLoading = false;
    private int sceneToLoad = -1;
    public GameObject text;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LoadingBar.value = 0.0f;
    }

    private void Update()
    {
        if (isLoading && LoadingBar.value >= 1.0f && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneToLoad));
        }
        if (LoadingBar.value >= 1.0f)
        {
            text.SetActive(true);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColisorChaoParaOJoseph")
        {
            playerMove.isGrounded = true;
        }
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "Grama")
        {
            audioSource.PlayOneShot(landingSound);
            playerMove.isGrounded = true;
        }        
        if (collision.gameObject.tag == "ground")
        {
            playerMove.estaNaPlataforma = true;
        }
        if (collision.gameObject.tag == "Grama")
        {
            playerMove.estaNaGrama = true;
        }
        if (collision.gameObject.tag == "Buraco")
        {
            playerMove.bigDamage();
            playerMove.isDeath = true;
        }
        if (collision.gameObject.tag == "BuracoFase2")
        {
            playerMove.bigDamage();
            playerMove.caiunoBuraco = true;
        }
        if (collision.gameObject.tag == "ProximaFase")
        {
            LoadScene(sceneId);
        }
        if (collision.gameObject.tag == "ProximaFase3")
        {
            LoadScene(sceneId);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColisorChaoParaOJoseph")
        {
            playerMove.isGrounded = false;
        }
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "Grama")
        {
            playerMove.isGrounded = false;
        }
        if (collision.gameObject.tag == "ground")
        {
            playerMove.estaNaPlataforma = false;
        }
        if (collision.gameObject.tag == "Grama")
        {
            playerMove.estaNaGrama = false;
        }
    }
}
