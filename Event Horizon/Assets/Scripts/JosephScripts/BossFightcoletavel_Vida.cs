using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightcoletavel_Vida : MonoBehaviour
{
    public playerMove playerMove;
    public float respawnTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Coletado();

            RespawItem();
        }
    }
    void Coletado()
    {
        playerMove.currentHealth = playerMove.currentHealth + 100f;
        playerMove.healthBar.value = playerMove.currentHealth;
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
