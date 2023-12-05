using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GroundColisor : MonoBehaviour
{
    public Transform player;
    public playerMove playerMove;
    public int sceneId;
    public int cutScene;
    private AudioSource audioSource;
    public menu_Controller control;
    [SerializeField] private AudioClip landingSound;
    public bool fade1 = false;
    public lookAheadObject cameraPoste;
    private Transform currentCheckPoint;
    private Light2D luz2DOutroObjeto;
    public GameObject LightGameObject;
    public bool endTrigger = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject objetoComLight2D = GameObject.Find("part 3");
        luz2DOutroObjeto = objetoComLight2D.GetComponent<Light2D>();
    }

    public void Respawn()
    {
        playerMove.isDeath = false;
        playerMove.outHealth = false;
        playerMove.isDefending = false;
        playerMove.podeMover = true;
        player.transform.position = currentCheckPoint.position;
        playerMove.anim.SetBool("Death", false);
        playerMove.currentHealth = 100f;
        playerMove.healthBar.value = playerMove.currentHealth;
        playerMove.currentStamina = 100f;
        playerMove.slider.value = playerMove.currentStamina;
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
            fade1 = true;
        }
        if (collision.gameObject.tag == "BossFurao")
        {
            fade1 = true;
        }
        if (collision.gameObject.tag == "ProximaFase4")
        {
            fade1 = true;
        }
        if (collision.gameObject.tag == "ProximaFase3")
        {
            fade1 = true;
        }
        if (collision.gameObject.tag == "cutScene")
        {
            SceneManager.LoadScene(cutScene);
        }
        if (collision.gameObject.tag == "fase3.1")
        {
            Invoke("GoFase3", 0.5f);
        }
        if (collision.gameObject.tag == "Checkpoint")
        {
            if(collision.gameObject.name == "CheckPointComLuz")
            {
                if(LightGameObject != null)
                {
                    print("ligou");
                    LightGameObject.SetActive(true);
                }
                luz2DOutroObjeto.intensity = 0.96f;
                currentCheckPoint = collision.transform;
            }
            else
            {
                currentCheckPoint = collision.transform;
            }
        }
        if(collision.gameObject.tag == "endGame")
        {
            endTrigger = true;
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
    void GoFase3()
    {
        SceneManager.LoadScene("Fase3 1");
    }
}

