using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisorEnemy : MonoBehaviour
{
    public EnemyTronco enemyTronco;
    public playerMove playerMove;
    public bool alert = false;
    public bool inRange;
    public bool isPlayerDefending = false;

    private void Update()
    {
        if (enemyTronco.isAttack)
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
            alert = true;
            inRange = true;           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
            Invoke("AlertFalse", 0.5f);
        }
    }   
    void AlertFalse()
    {
        alert = false;
    }
}
