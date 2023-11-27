using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class endGameController : MonoBehaviour
{
    public VideoPlayer endVideo;
    public GameObject EndVideo;
    public GameObject SkipVideo;
    public GameObject Canvas;
    public GroundColisor GroundColisor;
    private bool end = false;

    void OnVideoStarted(VideoPlayer vp)
    {
        Canvas.SetActive(false);
        SkipVideo.SetActive(true);
        end = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            end = true;
        }

        if (GroundColisor.endTrigger)
        {
            Canvas.SetActive(false);
            EndVideo.SetActive(true);
            SkipVideo.SetActive(true);
            endVideo.started += OnVideoStarted;
            endVideo.loopPointReached += OnVideoFinished;
        }
        if (end)
        {
            GroundColisor.endTrigger = false;
            SceneManager.LoadSceneAsync(0);
        }
    }
    void OnVideoFinished(VideoPlayer vp)
    {
        end = true;
    }
}
