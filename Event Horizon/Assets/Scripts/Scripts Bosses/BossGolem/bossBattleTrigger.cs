using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class bossBattleTrigger : MonoBehaviour
{
    public bossGolemController bossGolemController;
    private int trigger = 1;

    public CinemachineVirtualCamera virtualCamera; 
    public float increaseSpeed; 
    public float maxSize;
    public bool increaseSize = false;

    private float initialSize;
    private bool isIncreasing = false;
    public bool bossCamera = false;
    public GameObject healthBar;
    public CinemachineConfiner confiner;

    private void Start()
    {
        if (virtualCamera != null)
        {
            initialSize = virtualCamera.m_Lens.OrthographicSize;
        }
    }
    private void Update()
    {
        if (increaseSize && !isIncreasing)
        {
            StartCoroutine(IncreaseCameraSize());
        }
    }

    private IEnumerator IncreaseCameraSize()
    {
        isIncreasing = true;

        while (virtualCamera.m_Lens.OrthographicSize < maxSize)
        {
            float newSize = virtualCamera.m_Lens.OrthographicSize + increaseSpeed * Time.deltaTime;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Min(newSize, maxSize);
            yield return null;
        }

        isIncreasing = false;
        increaseSize = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (trigger == 1)
            {
                healthBar.SetActive(true);
                bossCamera = true;
                trigger = 0;
                bossGolemController.rugido = true;
                increaseSize = true;
                confiner.enabled = false;
            }
        }
    }
}
