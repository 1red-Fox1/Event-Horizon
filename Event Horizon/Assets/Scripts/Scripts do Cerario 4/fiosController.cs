using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fiosController : MonoBehaviour
{
    private Animator anim;
    private bool isActivated = false;

    private void Start()
    {
        anim = GetComponent <Animator>();
        StartCoroutine(UpdateActivationInterval());
    }

    private System.Collections.IEnumerator UpdateActivationInterval()
    {
        while (true)
        {
            float activationInterval = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(activationInterval);
            isActivated = !isActivated;
            anim.SetBool("Activated", isActivated);
        }
    }
}