using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coletavel_Vida : MonoBehaviour
{
    public playerMove playerMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerMove.currentHealth = playerMove.currentHealth + 100f;
            playerMove.healthBar.value = playerMove.currentHealth;
            Destroy(gameObject);
        }
    }
}
