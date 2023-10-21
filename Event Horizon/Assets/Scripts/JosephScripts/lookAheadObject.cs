using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAheadObject : MonoBehaviour
{
    public Transform referenceObject;
    public float moveSpeed;
    private bool podeIr = true;
    private bool isMoving = false;
    private Vector3 initialPosition;
    private float timeLimitLookAhead;
    public float timeLimit;
    public menu_Controller control;

    public bool poste1 = false;
    public Transform Poste1;
    public bool poste2 = false;
    public Transform Poste2;
    public bool poste3 = false;
    public Transform Poste3;
    public bool poste4 = false;
    public Transform Poste4;
    public bool poste5 = false;
    public Transform Poste5;
    public bool poste6 = false;
    public Transform Poste6;
    public bool poste7 = false;
    public Transform Poste7;
    public bool poste8 = false;
    public Transform Poste8;
    public bool poste9 = false;
    public Transform Poste9;
    private void Start()
    {
        initialPosition = transform.position;
        timeLimitLookAhead = 0f;
    }

    private void Update()
    {
        if (control.defaultControl)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving)
            {
                isMoving = true;
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                timeLimitLookAhead = 0f;
                isMoving = false;
                transform.position = new Vector3(transform.position.x, referenceObject.position.y, transform.position.z);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S) && !isMoving)
            {
                isMoving = true;
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                timeLimitLookAhead = 0f;
                isMoving = false;
                transform.position = new Vector3(transform.position.x, referenceObject.position.y, transform.position.z);
            }
        }

        if (isMoving)
        {
            if (podeIr)
            {
                timeLimitLookAhead += Time.deltaTime;
                if(timeLimitLookAhead >= timeLimit)
                {
                    transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                }
            }
        }
        #region Postes

        float poste1Position = referenceObject.transform.position.x - Poste1.transform.position.x;
        float poste2Position = referenceObject.transform.position.x - Poste2.transform.position.x;
        float poste3Position = referenceObject.transform.position.x - Poste3.transform.position.x;
        float poste4Position = referenceObject.transform.position.x - Poste4.transform.position.x;
        float poste5Position = referenceObject.transform.position.x - Poste5.transform.position.x;
        float poste6Position = referenceObject.transform.position.x - Poste6.transform.position.x;
        float poste7Position = referenceObject.transform.position.x - Poste7.transform.position.x;
        float poste8Position = referenceObject.transform.position.x - Poste8.transform.position.x;
        float poste9Position = referenceObject.transform.position.x - Poste9.transform.position.x;
        print(poste3Position);

        if(poste1Position >= -6f && poste1Position < 0f)
        {
            poste1 = true;
        }
        else
        {
            poste1 = false;
        }

        if (poste2Position >= -4.7f && poste2Position < 0f)
        {
            poste2 = true;
        }
        else
        {
            poste2 = false;
        }

        if (poste3Position >= -6f && poste3Position < 0f)
        {
            poste3 = true;
        }
        else
        {
            poste3 = false;
        }

        if (poste4Position >= -6f && poste4Position < 0f)
        {
            poste4 = true;
        }
        else
        {
            poste4 = false;
        }

        if (poste5Position >= -6f && poste5Position < 0f)
        {
            poste5 = true;
        }
        else
        {
            poste5 = false;
        }
        if (!isMoving)
        {
            if (poste1 && !(poste2 && poste3 && poste4 && poste5 && poste6 && poste7 && poste8 && poste9))
            {
                transform.position = new Vector3(Poste1.position.x, transform.position.y, transform.position.z);
            }
            else if (poste2 && !(poste1 && poste3 && poste4 && poste5 && poste6 && poste7 && poste8 && poste9))
            {
                transform.position = new Vector3(Poste2.position.x, transform.position.y, transform.position.z);
            }
            else if (poste3 && !(poste1 && poste2 && poste4 && poste5 && poste6 && poste7 && poste8 && poste9))
            {
                transform.position = new Vector3(Poste3.position.x, transform.position.y, transform.position.z);
            }
            else if (poste4 && !(poste1 && poste2 && poste3 && poste5 && poste6 && poste7 && poste8 && poste9))
            {
                transform.position = new Vector3(Poste4.position.x, transform.position.y, transform.position.z);
            }
            else if (poste5 && !(poste1 && poste2 && poste3 && poste4 && poste6 && poste7 && poste8 && poste9))
            {
                transform.position = new Vector3(Poste5.position.x, transform.position.y, transform.position.z);
            }
            else if (poste6 && !(poste1 && poste2 && poste3 && poste4 && poste5 && poste7 && poste8 && poste9))
            {
                transform.position = new Vector3(Poste6.position.x, transform.position.y, transform.position.z);
            }
            else if (poste7 && !(poste1 && poste2 && poste3 && poste4 && poste5 && poste6 && poste8 && poste9))
            {
                transform.position = new Vector3(Poste7.position.x, transform.position.y, transform.position.z);
            }
            else if (poste8 && !(poste1 && poste2 && poste3 && poste4 && poste5 && poste6 && poste7 && poste9))
            {
                transform.position = new Vector3(Poste8.position.x, transform.position.y, transform.position.z);
            }
            else if (poste9 && !(poste1 && poste2 && poste3 && poste4 && poste5 && poste6 && poste7 && poste8))
            {
                transform.position = new Vector3(Poste9.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(referenceObject.position.x, referenceObject.position.y, transform.position.z);
            }
        }
        else
        {
            transform.position = new Vector3(referenceObject.position.x, referenceObject.position.y, transform.position.z);
        }
        #endregion
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("limitLookAhead"))
        {
            podeIr = false;
        }       
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("limitLookAhead"))
        {
            podeIr = true;
        }
    }
}
