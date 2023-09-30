using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisorEstalactite : MonoBehaviour
{
    public bool inRange = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExite2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
