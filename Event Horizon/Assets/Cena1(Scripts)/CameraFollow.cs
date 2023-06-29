using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade de movimento do objeto

    private bool viradoParaDireita = true; // Indica se o objeto está virado para a direita

    private void Update()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");

        // Move o objeto na direção do movimento horizontal
        transform.Translate(Vector2.right * movimentoHorizontal * velocidade * Time.deltaTime);

        // Verifica se é necessário inverter a rotação
        if (movimentoHorizontal > 0 && !viradoParaDireita)
        {
            Flip();
        }
        else if (movimentoHorizontal < 0 && viradoParaDireita)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Inverte a escala do objeto no eixo X para inverter a rotação
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        // Atualiza o estado do objeto para virado para o lado oposto
        viradoParaDireita = !viradoParaDireita;
    }

}
