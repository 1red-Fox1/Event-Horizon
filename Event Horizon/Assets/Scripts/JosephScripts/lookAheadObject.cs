using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAheadObject : MonoBehaviour
{
    public Transform referenceObject;
    public Transform limitLookAhead;
    public float maxYLimit;
    public float moveSpeed;
    private bool isMoving = false;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float limitDistance = limitLookAhead.position.y;
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving)
        {
            float distance = initialPosition.y - transform.position.y;

            if (distance >= limitDistance)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow) && isMoving)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isMoving = false;
            transform.position = new Vector3(transform.position.x, referenceObject.position.y, transform.position.z);
        }
    }
}
