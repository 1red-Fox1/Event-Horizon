using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTronco : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public float speed;

    public Transform rightCol;
    public Transform leftCol;

    private bool colliding;

    public LayerMask layer;
    private bool tempoAcabou = false;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        float moveX = Input.GetAxisRaw("Horizontal");

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding)
        {
            StartCoroutine(ExecutarAposEspera());
            tempoAcabou = true;
        }

        if (moveX != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (tempoAcabou == true)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
            tempoAcabou = false;
        }
    }

    private IEnumerator ExecutarAposEspera()
    {
        // Espera de 2 segundos
        yield return new WaitForSeconds(2f);
        Debug.Log("O tempo de espera acabou!");       
    }
}

