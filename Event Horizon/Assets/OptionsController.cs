using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public GameObject Ok1;
    public GameObject Ok2;
    public GameObject FullScreenYes;
    public GameObject FullScreenNo;
    public menu_Controller options;

    void Update()
    {
        if (options.defaultControl)
        {
            Ok1.SetActive(true);
            Ok2.SetActive(false);
        }
        else
        {
            Ok1.SetActive(false);
            Ok2.SetActive(true);
        }

        if (options.fullScreen)
        {
            FullScreenYes.SetActive(true);
            FullScreenNo.SetActive(false);
        }
        else
        {
            FullScreenYes.SetActive(false);
            FullScreenNo.SetActive(true);
        }
    }
}
