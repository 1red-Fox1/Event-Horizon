using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackRangeRigSpider : MonoBehaviour
{
    public EnemyRigSpiderAnim rigSpider;
    public playerMove playerMove;
    public bool isInRange = false;

    private void Update()
    {
        if (rigSpider.isAttacking && isInRange)
        {
            if (!playerMove.isDefending && !playerMove.playerAttack && !playerMove.atacando)
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
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }
}
