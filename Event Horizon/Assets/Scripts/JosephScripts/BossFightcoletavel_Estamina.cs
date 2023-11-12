using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightcoletavel_Estamina : MonoBehaviour
{
    public playerMove playerMove;
    public float respawnTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Coletado();

            RespawItem();
        }
    }
    void Coletado()
    {
        playerMove.currentStamina = playerMove.currentStamina + 100f;
        playerMove.slider.value = playerMove.currentStamina;
        gameObject.SetActive(false);
    }
    void RespawItem()
    {
        Invoke("AtivarItem", respawnTime);
    }
    void AtivarItem()
    {
        gameObject.SetActive(true);
    }
}
