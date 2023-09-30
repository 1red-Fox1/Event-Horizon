using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilePrefab : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Grama" || collision.gameObject.tag == "ground" || collision.gameObject.tag == "projectile")
        {
            Destroy(gameObject);
        }
    }
    
    void Splash()
    {
        anim.SetBool("Splash", true);
    }
    void Destroy()
    {
         Destroy(gameObject);            
    }
}
