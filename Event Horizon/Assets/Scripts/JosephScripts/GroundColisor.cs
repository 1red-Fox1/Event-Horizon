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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneId));
    }

    IEnumerator LoadSceneAsyncCoroutine(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBar.value = progressValue;

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
