using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalController : MonoBehaviour
{
    private Animator anim;
    public bool alert;
    public AttackRangeCristal attack;
    public bool IsAttack;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (alert)
        {
            anim.SetBool("Activated", true);
        }
        else
        {
            anim.SetBool("Activated", false);
            anim.SetBool("Idle", false);
        }
        if (attack.canAttack)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    void endAnim()
    {
        anim.SetBool("Activated", false);
        anim.SetBool("Idle", true);
    }

    void IsAttacking()
    {
        IsAttack = true;
    }
    void IsNotAttacking()
    {
        IsAttack = false;
    }
    void EndAttack()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Idle", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            alert = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            alert = false;
        }
    }
}
