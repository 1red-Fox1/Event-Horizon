using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class lightBlink : MonoBehaviour
{
    public float minBlinkInterval = 1f;
    public float maxBlinkInterval = 5f;
    public float originalLight;

    private Light2D lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light2D>();

        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minBlinkInterval, maxBlinkInterval));

            lightComponent.intensity = 0f;
            yield return new WaitForSeconds(0.1f);

            lightComponent.intensity = originalLight;
        }
    }
}
