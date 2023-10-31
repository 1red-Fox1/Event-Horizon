using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMove : MonoBehaviour
{
    #region Variáveis do Player
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Animator anim;

    [Header("Variável de movimentação")]
    [SerializeField] public float moveSpeed;
    public bool facingRight;
    private float moveX;
    private float speedY;
    public bool isRunning = false;

    [Header("Variáveis de pulo do player")]
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float counterJump = 0.25f;
    public bool isGrounded;
    private bool isJumping;
    public float maxFallSpeed;

    [Header("Sonoplastia do personagem")]
    [SerializeField] private AudioClip[] passosPlataforma;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip somdano;
    [SerializeField] private AudioClip sommorte;
    [SerializeField] private AudioClip landingSound;
    public bool estaNaPlataforma = false;
    public bool estaNaGrama = false;
    public bool passoGrama1 = false;

    [Header("Stamina do personagem")]
    public Slider slider;
    public float maxStamina = 100f;
    public float currentStamina;
    public bool outStamina = false;
    public bool runAnimation = false;

    [Header("HP do personagem")]
    public Slider healthBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public bool outHealth = false;

    public float KBForce;
    public float KBForceY;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;
    public bool podeMover = true;
    public bool isDefending = false;
    public string gameOver;
    public bool levandoDano = false;

    public float forçaRecuo;
    public float counterRecuo;
    public float tempoTotalRecuo;
    public bool RecuoKnockFromRight;
    public EnemyTronco enemyTronco;
    public bool isDeath = false;
    public bool caiunoBuraco = false;
    public bool encimaDoInimigo = false;

    public bool playerAttack = false;
    public int combo;
    public bool atacando;
    public float attackStamina;
    public bool semStamina = false;
    public menu_Controller control;
    public bool atk1 = false;
    public bool atk2 = false;
    public bool atk3 = false;
    public bool canGrapp = false;
    public acidPipe acidPipe;
    private bool isLook;
    private Vector2 previousPosition;
    #endregion

    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        currentStamina = maxStamina;
        slider.maxValue = maxStamina;
        slider.value = currentStamina;
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        facingRight = true;       
    }
    void Update()
    {
        #region Input de Movimento
        moveX = 0f;
        if (podeMover)
        {
            if (control.defaultControl)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    moveX = -1f;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    moveX = 1f;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    moveX = 1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    moveX = -1f;
                }
            }
        }
        else
        {
            moveSpeed = 0f;
            moveX = 0f;
        }
        speedY = rb.velocity.y;

        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        #endregion

        #region Mudar de fase
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Fase1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Fase2.0");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Fase3");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("Fase4");
        }
        #endregion

        #region Defesa do personagem
        if (control.defaultControl)
        {
            if (Input.GetKeyDown(KeyCode.Z) && isGrounded && !isDeath)
            {
                anim.SetBool("DefenseOn", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                isDefending = true;
                podeMover = false;
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                anim.SetBool("DefenseOn", false);
                podeMover = true;
                isDefending = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J) && isGrounded && !isDeath)
            {
                anim.SetBool("DefenseOn", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                isDefending = true;
                podeMover = false;
            }
            if (Input.GetKeyUp(KeyCode.J))
            {
                anim.SetBool("DefenseOn", false);
                podeMover = true;
                isDefending = false;
            }
        }
        #endregion

        previousPosition = transform.position;

        if(Time.timeScale == 0f)
        {
            print("Pausado");
        }
        else
        {
            print(Time.timeScale);
        }

        #region Grapp Hook
        if (control.defaultControl)
        {
            if (Input.GetKeyDown(KeyCode.C) && isGrounded && !isDeath && moveX == 0f)
            {
                if(previousPosition.x != 0f)
                {
                    anim.SetBool("HookInAir", false);
                    anim.SetBool("Hook", true);
                }
                else
                {
                    anim.SetBool("HookInAir", true);
                }
            }
            if (Input.GetKeyUp(KeyCode.C) || moveX != 0f)
            {
                anim.SetBool("HookInAir", false);
                anim.SetBool("Hook", false);
                canGrapp = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.L) && isGrounded && !isDeath && moveX == 0f)
            {
                anim.SetBool("Hook", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
            }
            if (Input.GetKeyUp(KeyCode.L) || moveX != 0f)
            {
                anim.SetBool("Hook", false);
                canGrapp = false;
            }
        }       
        #endregion

        #region Personagem olhando pra baixo
        if (control.defaultControl)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded && !isDeath && !canGrapp)
            {
                anim.SetBool("LookAhead", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                podeMover = false;
                isLook = true;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow) || canGrapp)
            {
                anim.SetBool("LookAhead", false);
                podeMover = true;
                isLook = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S) && isGrounded && !isDeath && !canGrapp)
            {
                anim.SetBool("LookAhead", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                podeMover = false;
                isLook = true;
            }
            if (Input.GetKeyUp(KeyCode.S) || canGrapp)
            {
                anim.SetBool("LookAhead", false);
                podeMover = true;
                isLook = false;
            }
        }
        #endregion

        #region Ataque do personagem
        if (control.defaultControl)
        {
            if (Input.GetKeyDown(KeyCode.X) && !atacando && !isDeath && currentStamina >= 11f)
            {
                currentStamina = currentStamina - attackStamina;
                slider.value = currentStamina;
                atacando = true;
                anim.SetTrigger("" + combo);
            }
            if (Input.GetKeyDown(KeyCode.X) && currentStamina <= 10f)
            {
                semStamina = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.K) && !atacando && !isDeath && currentStamina >= 10f)
            {
                currentStamina = currentStamina - attackStamina;
                slider.value = currentStamina;
                atacando = true;
                anim.SetTrigger("" + combo);
            }
            if (Input.GetKeyDown(KeyCode.K) && currentStamina <= 9f)
            {
                semStamina = true;
            }
        }       
        #endregion

        #region Flip do Personagem
        if (moveX < 0)
        {
            UnFlip();
        }
        else if (moveX > 0)
        {
            Flip();
        }
        #endregion

        #region Animação de Andar
        if (moveX != 0 && isGrounded && !atacando)
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

        if((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && outStamina)
        {
            semStamina = true;
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

        #region HP do personagem
        if (currentHealth > 100f)
        {
            currentHealth = 100f;
        }
        if (currentHealth < 0f)
        {
            currentHealth = 0f;
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
        if (control.defaultControl)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) && !encimaDoInimigo && !isJumping)
            {
                if (isGrounded && !isDeath && !isDefending && !atacando && !isLook)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) && !encimaDoInimigo && !isJumping)
            {
                if (isGrounded && !isDeath && !isDefending && !atacando)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                }
            }
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Recuo do player
        if (KBCounter <= 0)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);            
        }
        else
        {
            if (!isDefending)
            {
                if(KnockFromRight == true && !isDeath)
                {
                    rb.velocity = new Vector2(-KBForce, KBForceY);
                    if (facingRight)
                    {
                        Flip();
                        facingRight = false;
                    }
                    isGrounded = false;
                    podeMover = false;
                    smallDamage();
                }
                if(KnockFromRight == false && !isDeath)
                {
                    rb.velocity = new Vector2(KBForce, KBForceY);
                    if (!facingRight)
                    {
                        Flip();
                        facingRight = true;
                    }
                    isGrounded = false;
                    podeMover = false;
                    smallDamage();
                }
                KBCounter -= Time.deltaTime;

                anim.SetBool("Damage", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", false);
                anim.SetBool("Hook", false);
            }
        }
        if(isGrounded && !isDefending && !isDeath && !isLook)
        {
            anim.SetBool("Damage", false);
            podeMover = true;
        }
        if (outHealth && (isGrounded || caiunoBuraco))
        {
            podeMover = false;
            anim.SetBool("Death", true);
        }
        #endregion

        #region Pulo
        if (isJumping && !isDeath && !isDefending && !isLook)
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
        #endregion

        #region Corrida
        if (isRunning == true && moveX != 0)
        {
            currentStamina -= 30f * Time.deltaTime;
            slider.value = currentStamina;
        }

        if (isRunning == false || moveX == 0)
        {
            currentStamina += 5f * Time.deltaTime;
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
        #endregion

        #region HP do personagem
        if (currentHealth <= 0f)
        {
            outHealth = true;
        }

        if (currentHealth > 0f)
        {
            outStamina = false;
        }
        #endregion        
    }

    #region Flip do Player
    void Flip()
    {
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    void UnFlip()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    #endregion

    #region Colisão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "projectile" && !isDefending && !atacando)
        {
            if (!isDeath)
            {
                KBCounter = KBTotalTime;
                if (collision.transform.position.x <= transform.position.x)
                {
                    KnockFromRight = true;
                }
                if (collision.transform.position.x > transform.position.x)
                {
                    KnockFromRight = false;
                }
            }
        }               
    }
    #endregion

    #region Metodos chamados nas Animações 

    void CanGrapp()
    {
        canGrapp = true;
    }
    private void JumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    private void Passos()
    {
        if (estaNaGrama)
        {
            passoGrama1 = true;
        }

        if (estaNaPlataforma)
        {
            audioSource.PlayOneShot(passosPlataforma[Random.Range(0, passosPlataforma.Length)]);
        }
    }
    private void somDano()
    {
        audioSource.PlayOneShot(somdano);
    }
    private void somMorte()
    {
        audioSource.PlayOneShot(sommorte);
    }
    private void somAtk1()
    {
        atk1 = true;
    }
    private void somAtk2()
    {
        atk2 = true;
    }
    private void somAtk3()
    {
        atk3 = true;
    }
    void deathTrigger()
    {
        isDeath = true;
    }
    public void Start_Combo()
    {
        atacando = false;
        if (combo < 3)
        {
            combo++;
        }
    }
    public void Finish_Anim()
    {
        atacando = false;
        combo = 0;
    }

    void isAttack()
    {
        playerAttack = true;
    }
    void isNotAttack()
    {
        playerAttack = false;
    }
    void PodeMover()
    {
        podeMover = true;
    }
    void NaoPodeMover()
    {
        if (!isGrounded)
        {
            podeMover = true;
        }
        else
        {
            podeMover = false;
        }
        
    }
    void stopDamageAnim()
    {
        anim.SetBool("Damage", false);
        podeMover = true;
    }
    #endregion

    #region HP
    public void smallDamage()
    {
        currentHealth = currentHealth - 1f;
        healthBar.value = currentHealth;
    }
    public void bigDamage()
    {
        currentHealth = 0f;
        healthBar.value = currentHealth;
    }
    #endregion
}
