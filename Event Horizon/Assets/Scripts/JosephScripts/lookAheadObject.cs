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
