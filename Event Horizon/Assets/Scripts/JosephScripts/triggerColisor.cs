using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class triggerColisor : MonoBehaviour
{
    public bossGolemController bossGolemController;
    public playerMove playerMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bossGolem" && !playerMove.isDefending && bossGolemController.canAttack)
        {
            if (!playerMove.isDeath)
            {
                playerMove.KBCounter = playerMove.KBTotalTime;
                if (collision.transform.position.x >= transform.position.x)
                {
                    playerMove.KnockFromRight = true;
                }
                if (collision.transform.position.x < transform.position.x)
                {
                    playerMove.KnockFromRight = false;
                }
            }
        }
    }
}
