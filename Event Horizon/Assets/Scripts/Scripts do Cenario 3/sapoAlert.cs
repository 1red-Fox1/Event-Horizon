using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sapoAlert : MonoBehaviour
{
    public bool alert = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            alert = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            alert = false;
        }
    }
}
