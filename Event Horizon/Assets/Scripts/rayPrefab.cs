using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayPrefab : MonoBehaviour
{    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
