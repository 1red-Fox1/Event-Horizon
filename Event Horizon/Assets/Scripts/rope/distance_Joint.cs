using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distance_Joint : MonoBehaviour
{
    public float speed; // velocidade de diminui��o da dist�ncia
    public float minDistance; // dist�ncia m�nima permitida

    private SpringJoint joint; // refer�ncia � junta

    void Start()
    {
        joint = GetComponent<SpringJoint>(); // obt�m a refer�ncia � junta
    }

    void Update()
    {
        if (joint == null) return; // verifica se a junta foi obtida

        // diminui a dist�ncia gradualmente
        joint.maxDistance -= speed * Time.deltaTime;

        // verifica se a dist�ncia atingiu o valor m�nimo
        if (joint.maxDistance < minDistance)
        {
            joint.maxDistance = minDistance;
        }
    }
}
