using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipController : MonoBehaviour
{
    public videosController videosController;

    public GameObject objectToDisable;
    public GameObject videoIntroDisable;
    public GameObject videoPosMenuDisable;
    public GameObject skipLabel;
    public int currentSkip = 0;
    public loadingController loadingController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && objectToDisable.activeSelf == false && currentSkip < 3)
        {
            currentSkip++;
        }
        if (currentSkip == 1)
        {
            currentSkip++;
            videoIntroDisable.SetActive(false);
            objectToDisable.SetActive(true);
            skipLabel.SetActive(false);
        }
        if(currentSkip == 3)
        {
            videoIntroDisable.SetActive(false);
            videoPosMenuDisable.SetActive(false);
            skipLabel.SetActive(false);
            objectToDisable.SetActive(true);
            loadingController.StartLoading(1);
        }
    }
}
