using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderAlertColisor : MonoBehaviour
{
    public bool spiderAlert = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            spiderAlert = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spiderAlert = false;
        }
    }
}
