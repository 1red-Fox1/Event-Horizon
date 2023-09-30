using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coletavel_Estamina : MonoBehaviour
{
    public playerMove playerMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMove.currentStamina = playerMove.currentStamina + 100f;
            playerMove.slider.value = playerMove.currentStamina;
            Destroy(gameObject);
        }
    }
}
