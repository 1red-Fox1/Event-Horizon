using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesactiveScript : MonoBehaviour
{
    public MonoBehaviour scriptToActivate;
    public Text textToHide;

    private bool isScriptActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isScriptActive = !isScriptActive;
            scriptToActivate.enabled = isScriptActive;
            textToHide.enabled = !isScriptActive;
        }
    }
}





