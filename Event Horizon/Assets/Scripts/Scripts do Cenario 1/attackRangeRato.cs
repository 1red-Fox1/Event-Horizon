using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackRangeRato : MonoBehaviour
{
    public RatoController ratoController;
    public playerMove playerMove;
    public bool attackRange = false;

    private void Update()
    {
        if (ratoController.isAttack)
        {
            if (attackRange && !playerMove.isDefending && !playerMove.playerAttack && !playerMove.atacando)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            attackRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackRange = false;
        }
    }
}
