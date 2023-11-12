using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySpider : MonoBehaviour
{
    public bool podeDestruir = false;
    public bossAranhaGigante aranhaGigante;
    public float timer;

    private void Start()
    {
        timer = 0f;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        print(timer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "aranhaGigante")
        {
            print("sçdansan");
            aranhaGigante.naoPodeAtacar = true;
        }
    }
}
