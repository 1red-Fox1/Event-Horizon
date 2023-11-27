using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videosController : MonoBehaviour
{
    public VideoPlayer introVideo;
    public VideoPlayer posMenuVideo;
    private bool inicioPosMenuVideo = false;
    public skipController skipController;
    public GameObject skipLabel;
    void Start()
    {
        introVideo.started += OnVideoStarted;
        introVideo.loopPointReached += OnVideoFinished;
    }
    private void Update()
    {        
        if (inicioPosMenuVideo)
        {
            posMenuVideo.started += OnVideoStarted;
            posMenuVideo.loopPointReached += OnPosMenuVideoFinished;
        }
    }
    public void videoTrigger()
    {
        skipController.objectToDisable.SetActive(false);
        inicioPosMenuVideo = true;
        skipController.videoPosMenuDisable.SetActive(true);
        skipController.videoIntroDisable.SetActive(true);
        posMenuVideo.Play();
        skipLabel.SetActive(true);
    }

    void OnVideoStarted(VideoPlayer vp)
    {
        if (skipController.objectToDisable != null)
        {
            skipLabel.SetActive(true);
            skipController.objectToDisable.SetActive(false);
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (skipController.objectToDisable != null)
        {
            skipController.objectToDisable.SetActive(true);
        }
    }
    void OnPosMenuVideoFinished(VideoPlayer vp)
    {
        if (skipController.objectToDisable != null)
        {
            skipController.loadingController.StartLoading(1);
            skipController.videoIntroDisable.SetActive(false);
            skipController.videoPosMenuDisable.SetActive(false);
            skipController.objectToDisable.SetActive(true);
        }
    }
}

