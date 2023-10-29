using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisorEnemyBarata : MonoBehaviour
{
    public barataComtroller enemyBarata;
    public playerMove playerMove;
    public bool inRange;
    public bool isPlayerDefending = false;

    private void Update()
    {
        if (enemyBarata.isAttack)
        {
            if (inRange && !playerMove.isDefending && !playerMove.playerAttack && !playerMove.atacando)
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
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }

}
