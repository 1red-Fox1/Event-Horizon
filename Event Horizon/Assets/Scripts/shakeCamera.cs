using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class shakeCamera : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cbmcp;
    public float ShakeIntensity;
    public float ShakeTime;
    public float timer;
    public bossGolemController bossGolemController;
    public enxameCupins enxameCupins;
    public furaoController1 furaoController1;


    void Awake()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        StopShake();
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;

        timer = ShakeTime;
    }

    void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
        timer = 0;
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Fase4")
        {
            if (bossGolemController.shake)
            {
                ShakeCamera();
            }
            else
            {
                StopShake();
            }
        }

        if(currentScene.name == "Fase3 1")
        {
            if (enxameCupins.shakeFase3)
            {
                ShakeCamera();
            }
            else
            {
                StopShake();
            }
        }

        if (currentScene.name == "BossFight1")
        {
            if (furaoController1.rugido)
            {
                ShakeCamera();
            }
            else
            {
                StopShake();
            }
        }
    }
}
