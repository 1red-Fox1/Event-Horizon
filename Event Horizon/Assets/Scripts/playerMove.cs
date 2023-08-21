using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public string sceneName;
    public bool isRunning = false;

    [Header("Variáveis de pulo do player")]
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float counterJump = 0.25f;
    public bool isGrounded;
    private bool isJumping;
    private bool hasJumped;
    public float maxFallSpeed;

    [Header("Sonoplastia do personagem")]
    [SerializeField] private AudioClip[] passosGrama;
    [SerializeField] private AudioClip[] passosPlataforma;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landingSound;
    private bool estaNaPlataforma = false;
    private bool estaNaGrama = false;

    [Header("Stamina do personagem")]
    public Slider slider;
    public float maxStamina = 100f;
    public float currentStamina;
    public bool outStamina = false;
    public bool runAnimation = false;

    public bool DefenseCooling = false;
    public bool CanMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        currentStamina = maxStamina;
        slider.maxValue = maxStamina;
        slider.value = currentStamina;
        facingRight = true;
    }

    void Update()
    {
        if (CanMove)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            speedY = rb.velocity.y;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }

        if(Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            anim.SetBool("DefenseOn", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            CanMove = false;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("DefenseOn", false);
            anim.SetBool("DefenseOff", true);
            if (DefenseCooling == true)
            {
                anim.SetBool("DefenseOff", false);
                anim.SetBool("DefenseOn", false);
                DefenseCooling = false;
                CanMove = true;
            }
        }

        #region Flip do Personagem
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
        #endregion

        #region Animação de Andar
        if (moveX != 0 && isGrounded)
        {
            if (runAnimation)
            {
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }
        #endregion

        #region Animação de Correr do Personagem
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isRunning = false;
        }

        if (currentStamina > 100f)
        {
            currentStamina = 100f;
        }
        if (currentStamina < 0f)
        {
            currentStamina = 0f;
        }
        #endregion

        #region Animação de Pulo e Queda em movimento
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
        #endregion      

        #region Pulo do Personagem
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
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
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            counterJump -= Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            counterJump = 0.25f;
        }
        #endregion
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

        if (isRunning == true && moveX != 0)
        {
            currentStamina -= 40f * Time.deltaTime;
            slider.value = currentStamina;
        }

        if (isRunning == false || moveX == 0)
        {
            currentStamina += 35f * Time.deltaTime;
            slider.value = currentStamina;
        }

        if (currentStamina <= 0f)
        {
            outStamina = true;
        }

        if (currentStamina > 0f && isRunning == false)
        {
            outStamina = false;
        }

        if (isRunning == true || outStamina == false)
        {
            moveSpeed = 2.5f;
        }

        if (isRunning == false || outStamina == true)
        {
            moveSpeed = 1.2f;
        }
        
        if (moveSpeed == 2.5f)
        {
            runAnimation = true;
        }

        if (moveSpeed == 1.2f)
        {
            runAnimation = false;
        }

    }

    #region Flip do Player
    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    #endregion

    #region Colisão
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        hasJumped = false;

        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "Grama")
        {
            audioSource.PlayOneShot(landingSound);
        }
        if (collision.gameObject.tag == "Buraco")
        {
            SceneManager.LoadScene(sceneName);
        }
        if (collision.gameObject.tag == "ProximaFase")
        {
            SceneManager.LoadScene("Fase2");
        }
        if (collision.gameObject.tag == "ground")
        {
            estaNaPlataforma = true;
        }
        if (collision.gameObject.tag == "Grama")
        {
            estaNaGrama = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "Grama")
        {
            isGrounded = false;
        }
        if (collision.gameObject.tag == "ground")
        {
            estaNaPlataforma = false;
        }
        if (collision.gameObject.tag == "Grama")
        {
            estaNaGrama = false;
        }
    }
    #endregion

    #region Sonoplastia 
    private void Passos()
    {
        if (estaNaGrama)
        {
            audioSource.PlayOneShot(passosGrama[Random.Range(0, passosGrama.Length)]);
        }

        if (estaNaPlataforma)
        {
            audioSource.PlayOneShot(passosPlataforma[Random.Range(0, passosPlataforma.Length)]);
        }
    }
    #endregion

    void defenseCooling()
    {
        DefenseCooling = true;
    }
}
