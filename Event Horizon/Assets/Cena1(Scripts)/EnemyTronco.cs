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
    public LayerMask layer;

    private bool colliding;
    private bool tempoAcabou = false;
    private bool podeInverter = true;
    private int direcao = 1;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(direcao * speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding && podeInverter)
        {
            if (!tempoAcabou)
            {
                StartCoroutine(ExecutarAposEspera());
            }
        }

        if (colliding && Mathf.Approximately(rig.velocity.x, 0f))
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Walk", true);
        }
    }

    private IEnumerator ExecutarAposEspera()
    {
        speed = 0f;
        podeInverter = false; // Desativa a possibilidade de inverter a direção
        yield return new WaitForSeconds(2f);
        direcao *= -1; // Inverte a direção do movimento
        speed = Mathf.Abs(speed); // Garante que a velocidade seja positiva
        tempoAcabou = true;
        podeInverter = true; // Permite a inversão da direção novamente
        yield return new WaitForSeconds(0.1f); // Pequeno atraso para evitar inversões consecutivas
        speed *= direcao; // Define a velocidade de acordo com a direção atual
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rightCol.position, leftCol.position);
    }
}


