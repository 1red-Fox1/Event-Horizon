using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAranhaGigante : MonoBehaviour
{
    public GameObject[] spots;
    private float moveSpeed;

    private int currentSpotIndex = 0;
    private Transform currentSpot;

    public playerMove playerMove;
    public int damage;
    public float maxDistance;
    public float maxDistance2;
    public float velocity0;
    public float velocity1;
    public float velocity2;
    private bool damaged = false;
    void Start()
    {
        if (spots.Length > 0)
        {
            currentSpot = spots[0].transform;
        }
    }

    void Update()
    {
        float distance = Mathf.Abs(playerMove.transform.position.x - transform.position.x);

        if (distance >= maxDistance && distance < maxDistance2)
        {
            moveSpeed = velocity1;
        }
        if (distance >= maxDistance2)
        {
            moveSpeed = velocity2;
        }
        if(distance < maxDistance)
        {
            moveSpeed = velocity0;
        }

        if (playerMove.isDeath)
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.isTrigger = true;
            }
        }
        else
        {
            walk();
        }

        if (damaged)
        {
                playerMove.KBCounter = playerMove.KBTotalTime;
                if (playerMove.transform.position.x <= transform.position.x)
                {
                    playerMove.KnockFromRight = true;
                }
                if (playerMove.transform.position.x > transform.position.x)
                {
                    playerMove.KnockFromRight = false;
                }
                playerMove.currentHealth = playerMove.currentHealth - damage;
                playerMove.healthBar.value = playerMove.currentHealth;
            damaged = false;
        }
    }

    void walk()
    {
        if (currentSpot == null)
            return;

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, currentSpot.position, step);

        if (Vector2.Distance(transform.position, currentSpot.position) < 0.1f)
        {
            currentSpotIndex = (currentSpotIndex + 1) % spots.Length;
            currentSpot = spots[currentSpotIndex].transform;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            damaged = true;
        }
    }
}
