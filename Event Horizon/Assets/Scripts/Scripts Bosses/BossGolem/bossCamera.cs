using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCamera : MonoBehaviour
{
    public bossBattleTrigger bossBattleTrigger;
    public CinemachineVirtualCamera virtualCamera;
    public Transform defaultTarget;
    public Transform newTarget;
    public bool switchCameraTarget = false;

    void Start()
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = defaultTarget;
        }
    }

    void Update()
    {
        if (bossBattleTrigger.bossCamera)
        {
            virtualCamera.Follow = newTarget;
        }
        else
        {
            virtualCamera.Follow = defaultTarget;
        }
    }
}
