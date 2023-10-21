using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acidPipe : MonoBehaviour
{
    private Animator anim;
    private bool isActivated = false;
    public float activationInterval;

    private void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("ToggleActivation", 0.0f, activationInterval);
    }

    private void ToggleActivation()
    {
        isActivated = !isActivated;
        anim.SetBool("Activated", isActivated);
    }
}
