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
    private AudioSource audioSource;
    public AudioClip []stepSound;
    public AudioClip scream;
    public bool naoPodeAtacar = false;
    private float timer = 0f;
    void Start()
    {
        if (spots.Length > 0)
        {
            currentSpot = spots[0].transform;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(scream);
        InvokeRepeating("Scream", 7f, 7f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 34.5f)
        {
            naoPodeAtacar = true;
        }

        float distance = Mathf.Abs(playerMove.transform.position.x - transform.position.x);

        if (distance >= maxDistance && distance < maxDistance2 && !naoPodeAtacar)
        {
            moveSpeed = velocity1;
        }
        if (distance >= maxDistance2 && !naoPodeAtacar)
        {
            moveSpeed = velocity2;
        }
        if(distance < maxDistance && !naoPodeAtacar)
        {
            moveSpeed = velocity0;
        }

        if (!naoPodeAtacar)
        {
            walk();
        }
        else
        {
            moveSpeed = 0f;
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
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, currentSpot.position, step);

        if (Vector2.Distance(transform.position, currentSpot.position) < 0.1f)
        {
            currentSpotIndex = (currentSpotIndex + 1) % spots.Length;
            currentSpot = spots[currentSpotIndex].transform;
        }
    }

    void StepSound()
    {
        audioSource.PlayOneShot(stepSound[Random.Range(0, stepSound.Length)]);
    }
    void Scream()
    {
        audioSource.PlayOneShot(scream);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!naoPodeAtacar)
            {
                damaged = true;
            }
        }
    }
}
