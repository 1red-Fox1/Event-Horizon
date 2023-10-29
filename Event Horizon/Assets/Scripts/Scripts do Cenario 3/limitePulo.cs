using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitePulo : MonoBehaviour
{
    public playerMove playerMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerMove.KBCounter = playerMove.KBTotalTime;
            if (playerMove.transform.position.x <= transform.position.x)
            {
                playerMove.KnockFromRight = true;
            }
            if (playerMove.transform.position.x > transform.position.x)
            {
                playerMove.KnockFromRight = false;
            }
        }
    }
}
