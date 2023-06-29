using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Animator anim;

    [Header("Variável de movimentação")]
    [SerializeField] public float moveSpeed;
    private bool facingRight;
    private float moveX;
    private float speedY;

    [Header("Variáveis de pulo do player")]
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float counterJump = 0.25f;
    public bool isGrounded;
    private bool isJumping;
    private bool hasJumped;

    [Header("Sonoplastia do personagem")]
    [SerializeField] private AudioClip[] footstepSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landingSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();   
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        speedY = rb.velocity.y;

        if (moveX != 0 && isGrounded)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (moveX != 0 && !isGrounded)
        {
            anim.SetBool("Walk", false);
        }

        if (moveX < 0 && !facingRight)
        {
            Flip();
            facingRight = true;
        }
        else if (moveX > 0 && facingRight)
        {
            Flip();
            facingRight = false;
        }

        if (speedY > 0 && !isGrounded)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Fall", false);
        }
        else if (speedY < 0 && !isGrounded)
        {
            anim.SetBool("Fall", true);
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Fall", false);
            anim.SetBool("Jump", false);
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGrounded && !isJumping)
        {
            if (!hasJumped)
            {
                audioSource.clip = jumpSound;
                audioSource.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                isJumping = true;
                hasJumped = true;
            }
        }
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            counterJump -= Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            counterJump = 0.25f;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (isJumping)
        {
            if (counterJump > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            else
            {
                isJumping = false;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        hasJumped = false;

        if (collision.gameObject.tag == "ground")
        {
            audioSource.PlayOneShot(landingSound);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }

    private void Passos()
    {
        audioSource.PlayOneShot(footstepSound[Random.Range(0, footstepSound.Length)]);
    }
}
