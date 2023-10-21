using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GroundColisor : MonoBehaviour
{
    public playerMove playerMove;
    public int sceneId;
    public int cutScene;
    private AudioSource audioSource;
    public menu_Controller control;
    [SerializeField] private AudioClip landingSound;
    public bool fade1 = false;
    public lookAheadObject cameraPoste;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (collision.gameObject.tag == "ProximaFase3")
        {
            fade1 = true;
        }
        if(collision.gameObject.tag == "cutScene")
        {
            SceneManager.LoadScene(cutScene);
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
