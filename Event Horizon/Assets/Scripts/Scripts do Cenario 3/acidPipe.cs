using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acidPipe : MonoBehaviour
{
    private Animator anim;
    private bool isActivated = false;
    public float activationInterval;
    public bool isHit;
    private bool canHit;
    public playerMove playerMove;
    public float shakeMagnitude;
    public float shakeDuration;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
        anim = GetComponent <Animator>();
        InvokeRepeating("ShakeAndToggle", 0.0f, activationInterval);
    }

    private void Update()
    {
        if (isHit)
        {
            playerMove.canGrapp = false;
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

    private void ShakeAndToggle()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;
        Vector3 startPosition = transform.position;

        while (elapsed < shakeDuration)
        {
            Vector3 randomPosition = startPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.position = randomPosition;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;

        ToggleActivation();
    }

    public void CanHit()
    {
        canHit = true;
    }

    public void CantHit()
    {
        canHit = false;
    }

    private void ToggleActivation()
    {
        isActivated = !isActivated;
        anim.SetBool("Activated", isActivated);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canHit)
        {
            isHit = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isHit = false;
        }
    }
}
