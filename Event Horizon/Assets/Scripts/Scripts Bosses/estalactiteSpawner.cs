using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estalactiteSpawner : MonoBehaviour
{
    public GameObject estalactite;
    public scriptEstalactiteBoss est;

    public GameObject estalactite2;
    public scriptEstalactiteBoss est2;

    public GameObject estalactite3;
    public scriptEstalactiteBoss est3;

    public GameObject estalactite4;
    public scriptEstalactiteBoss est4;

    public GameObject estalactite5;
    public scriptEstalactiteBoss est5;

    public GameObject estalactite6;
    public scriptEstalactiteBoss est6;

    public GameObject estalactite7;
    public scriptEstalactiteBoss est7;

    public GameObject estalactite8;
    public scriptEstalactiteBoss est8;

    public GameObject estalactite9;
    public scriptEstalactiteBoss est9;

    public GameObject estalactite10;
    public scriptEstalactiteBoss est10;

    public GameObject estalactite11;
    public scriptEstalactiteBoss est11;

    void Update()
    {
        if (est.estalactiteDestruida)
        {
            Spawn2();
            est.estalactiteDestruida = false;
        }
        if (est2.estalactiteDestruida)
        {
            Spawn3();
            est2.estalactiteDestruida = false;
        }
        if (est3.estalactiteDestruida)
        {
            Spawn4();
            est3.estalactiteDestruida = false;
        }
        if (est4.estalactiteDestruida)
        {
            Spawn5();
            est4.estalactiteDestruida = false;
        }
        if (est5.estalactiteDestruida)
        {
            Spawn6();
            est5.estalactiteDestruida = false;
        }
        if (est6.estalactiteDestruida)
        {
            Spawn7();
            est6.estalactiteDestruida = false;
        }
        if (est7.estalactiteDestruida)
        {
            Spawn8();
            est7.estalactiteDestruida = false;
        }
        if (est8.estalactiteDestruida)
        {
            Spawn9();
            est8.estalactiteDestruida = false;
        }
        if (est9.estalactiteDestruida)
        {
            Spawn10();
            est9.estalactiteDestruida = false;
        }
        if (est10.estalactiteDestruida)
        {
            Spawn11();
            est10.estalactiteDestruida = false;
        }
    }

    void Spawn2()
    {
        estalactite2.SetActive(true);
    }

    void Spawn3()
    {
        estalactite3.SetActive(true);
    }

    void Spawn4()
    {
        estalactite4.SetActive(true);
    }

    void Spawn5()
    {
        estalactite5.SetActive(true);
    }

    void Spawn6()
    {
        estalactite6.SetActive(true);
    }

    void Spawn7()
    {
        estalactite7.SetActive(true);
    }

    void Spawn8()
    {
        estalactite8.SetActive(true);
    }

    void Spawn9()
    {
        estalactite9.SetActive(true);
    }

    void Spawn10()
    {
        estalactite10.SetActive(true);
    }

    void Spawn11()
    {
        estalactite11.SetActive(true);
    }
}
