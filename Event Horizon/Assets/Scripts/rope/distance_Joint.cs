using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distance_Joint : MonoBehaviour
{
    public float speed; // velocidade de diminuição da distância
    public float minDistance; // distância mínima permitida

    private SpringJoint joint; // referência à junta

    void Start()
    {
        joint = GetComponent<SpringJoint>(); // obtém a referência à junta
    }

    void Update()
    {
        if (joint == null) return; // verifica se a junta foi obtida

        // diminui a distância gradualmente
        joint.maxDistance -= speed * Time.deltaTime;

        // verifica se a distância atingiu o valor mínimo
        if (joint.maxDistance < minDistance)
        {
            joint.maxDistance = minDistance;
        }
    }
}
