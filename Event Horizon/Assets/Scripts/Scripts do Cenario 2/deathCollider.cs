using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCollider : MonoBehaviour
{
    public playerMove playerMove;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("sfdflj");
            playerMove.isDeath = true;
            playerMove.currentHealth = 0f;
            playerMove.healthBar.value = playerMove.currentHealth;
            playerMove.outHealth = true;
        }
    }
}
