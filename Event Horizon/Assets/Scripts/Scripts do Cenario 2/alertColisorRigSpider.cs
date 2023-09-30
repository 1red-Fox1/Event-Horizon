using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alertColisorRigSpider : MonoBehaviour
{
    public bool alert = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        alert = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        alert = false;
    }
}
