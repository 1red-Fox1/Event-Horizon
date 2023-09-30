using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shakeStaminaBar : MonoBehaviour
{
    public GameObject sliderObject;
    public float shakeDuration;
    public float shakeMagnitude;

    private Vector3 originalPosition;
    public playerMove playerMove;

    void Start()
    {
        originalPosition = sliderObject.transform.position;
    }

    private void Update()
    {
        if (playerMove.semStamina)
        {
            ShakeSlider();
            playerMove.semStamina = false;
        }
    }

    public void ShakeSlider()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;
        Vector3 startPosition = sliderObject.transform.position;

        while (elapsed < shakeDuration)
        {

            Vector3 randomPosition = startPosition + Random.insideUnitSphere * shakeMagnitude;
            sliderObject.transform.position = randomPosition;

            elapsed += Time.deltaTime;
            yield return null;
        }

        sliderObject.transform.position = originalPosition;
    }
}
