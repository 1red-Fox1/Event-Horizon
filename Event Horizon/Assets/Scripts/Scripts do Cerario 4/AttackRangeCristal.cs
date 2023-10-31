using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCristal : MonoBehaviour
{
    public bool canAttack;
    public playerMove playerMove;
    public CristalController cristal;

    private void Update()
    {
        if (cristal.IsAttack)
        {
            if (!playerMove.isDefending && !playerMove.playerAttack && !playerMove.atacando && canAttack)
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
            canAttack = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canAttack = false;
        }
    }
}
