using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapp : MonoBehaviour
{
    private float moveX;
    private float speedY;

    private Rigidbody2D rb;
    public float moveSpeed;
    public float jumpSpeed;

    public bool isGrounded;

    public float slowMotionTimeScale;
    public float slowMotionDuration;
    private bool isSlowMotionActive = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !isSlowMotionActive)
        {
            StartCoroutine(SlowMotionCoroutine());
        }


        moveX = Input.GetAxisRaw("Horizontal");
        speedY = rb.velocity.y;

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveX = 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveX = -1;
        }
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
        }       
    }
    private IEnumerator SlowMotionCoroutine()
    {
        isSlowMotionActive = true;
        Time.timeScale = slowMotionTimeScale;
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        Time.timeScale = 1f;
        isSlowMotionActive = false;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }
    void Jump()
    {
        rb.velocity = Vector2.up * jumpSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
}
