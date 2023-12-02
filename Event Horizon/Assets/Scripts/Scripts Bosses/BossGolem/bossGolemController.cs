using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class bossGolemController : MonoBehaviour
{
    private Animator anim;
    public Animator animCore;
    public playerMove playerMove;
    public bool rugido = false;
    private bool canAct = false;
    public int valorAleatorio = 0;
    public int attackCount = 6;
    public int idleCount = 4;
    private bool ataqueEmAndamento = false;
    private List<int> valoresSorteados = new List<int>();
    private bool vulnerabilityScheduled = false;
    private bool canAtkCount = false;


    const string RUGIDO_BOSS = "rugido_Boss";
    const string BOSS_IDLE = "boss_Idle";
    const string BOSS_ATTACK1 = "boss_Attack1";
    const string BOSS_ATTACK2 = "boss_Attack2";
    const string BOSS_ATTACK3 = "boss_Attack3";
    const string BOSS_ATTACK4 = "boss_Attack4";
    const string BOSS_ATTACK5 = "boss_Attack5";
    const string BOSS_VULNERABLE = "boss_Vulnerable";
    const string BOSS_DEFEATED = "boss_Defeated";
    public string currentState;
    public bool podeAgir = false;
    public bool podeIdle = false;

    public Material novoMaterial;
    public Material defaultMaterial;
    public SpriteRenderer core;
    public SpriteRenderer spike1;
    public SpriteRenderer spike2;
    public SpriteRenderer spike3;
    public SpriteRenderer rightArm;
    public SpriteRenderer leftArm;
    public float duracaoMudanca;
    public bool inRange = false;
    public bool endDamage = true;
    public bool canAttack = true;
    public PolygonCollider2D Spike1;
    public PolygonCollider2D Spike2;
    public PolygonCollider2D Spike3;
    public Animator healthBarAnim;
    public int healthBoss;
    public bool isDamaged = false;
    public int times = 1;
    public Sprite brokenRightArm;
    public Sprite brokenLeftArm;
    public bool endBossFight = false;
    public bool inicioBossFight = false;
    public bool shake = false;
    private AudioSource audioSource;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip Death;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        Spike1.enabled = false;
        Spike2.enabled = false;
        Spike3.enabled = false;
    }

    void Update()
    {
        if(healthBoss >= 2)
        {
            if (attackCount > 0)
            {
                if (healthBoss > 8)
                {
                    animCore.Play("boss_Core_Idle");
                }
                else
                {
                    animCore.Play("broken_Boss_Core_Idle");
                    rightArm.sprite = brokenRightArm;
                    leftArm.sprite = brokenLeftArm;
                }
            }
            if (rugido)
            {
                ChangeAnimationState(RUGIDO_BOSS);
                podeAgir = true;
                inicioBossFight = true;
            }
            else if (canAct && !ataqueEmAndamento && podeAgir)
            {
                RandomNum();

                if (idleCount <= 0)
                {
                    ChangeAnimationState(BOSS_IDLE);
                }
                else if (attackCount > 0)
                {
                    canAtkCount = true;
                    if (valorAleatorio == 1)
                    {
                        idleCount--;
                        attackCount--;
                        ataqueEmAndamento = true;
                        ChangeAnimationState(BOSS_ATTACK1);
                    }
                    else if (valorAleatorio == 2)
                    {
                        idleCount--;
                        attackCount--;
                        ataqueEmAndamento = true;
                        ChangeAnimationState(BOSS_ATTACK2);
                    }
                    else if (valorAleatorio == 3)
                    {
                        idleCount--;
                        attackCount--;
                        ataqueEmAndamento = true;
                        ChangeAnimationState(BOSS_ATTACK3);
                    }
                    else if (valorAleatorio == 4)
                    {
                        idleCount--;
                        attackCount--;
                        ataqueEmAndamento = true;
                        ChangeAnimationState(BOSS_ATTACK4);
                    }
                    else if (valorAleatorio == 5)
                    {
                        idleCount--;
                        attackCount--;
                        ataqueEmAndamento = true;
                        ChangeAnimationState(BOSS_ATTACK5);
                    }
                }
                canAct = true;
            }
            if (attackCount <= 0)
            {
                canAct = false;
                isDamaged = false;
                if (playerMove.playerAttack && inRange)
                {
                    isDamaged = true;
                }
                if (isDamaged && times == 1)
                {
                    times = 0;
                    IniciarMudancaDeMaterial();
                    healthBoss = healthBoss - 2;
                    healthBarDam();
                    Invoke("TimeCount", 1f);
                }
                ChangeAnimationState(BOSS_VULNERABLE);
                if (healthBoss > 8)
                {
                    animCore.Play("boss_Core_Open");
                }
                else
                {
                    animCore.Play("broken_boss_Core_Open");
                }
            }
            if (attackCount <= 0 && idleCount <= 0)
            {
                idleCount = 1;
            }
        }
        else
        {
            if(canAct && !ataqueEmAndamento && podeAgir)
            {
                deathAnim();
            }
        }
    }
    void deathAnim()
    {
        ChangeAnimationState(BOSS_DEFEATED);
        animCore.Play("boss_Core_Defeated");
        healthBarAnim.Play("fimHealthBar");
        if(times == 1)
        {
            times = 0;
            endBossFight = true;
        }
    }
    void Attack1Sound()
    {
        audioSource.PlayOneShot(attack1);
    }
    void Attack2Sound()
    {
        audioSource.PlayOneShot(attack2);
    }
    void DeathSound()
    {
        audioSource.PlayOneShot(Death);
    }
    void TimeCount()
    {
        times = 1;
    }
    void healthBarDam()
    {
        if(healthBoss == 16)
        {
            healthBarAnim.Play("healthBarValue16");
        }
        else if (healthBoss == 14)
        {
            healthBarAnim.Play("healthBarValue14");
        }
        else if (healthBoss == 12)
        {
            healthBarAnim.Play("healthBarValue12");
        }
        else if (healthBoss == 10)
        {
            healthBarAnim.Play("healthBarValue10");
        }
        else if (healthBoss == 8)
        {
            healthBarAnim.Play("HealthBarValue8");
        }
        else if (healthBoss == 6)
        {
            healthBarAnim.Play("HealthBarValue6");
        }
        else if (healthBoss == 4)
        {
            healthBarAnim.Play("HealthBarValue4");
        }
        else if (healthBoss == 2)
        {
            healthBarAnim.Play("HealthBarValue2");
        }
        else if (healthBoss == 0)
        {
            healthBarAnim.Play("healthBarValue0");
        }
    }
    void ShakeTrue()
    {
        shake = true;
    }
    void ShakeFalse()
    {
        shake = false;
    }
    void RandomNum()
    {
        valorAleatorio = GetUniqueRandom();
    }
    int GetUniqueRandom()
    {
        int novoValor = Random.Range(1, 7);

        while (valoresSorteados.Contains(novoValor))
        {
            novoValor = Random.Range(1, 7);
        }

        valoresSorteados.Add(novoValor);
      
        if (valoresSorteados.Count > 5)
        {
            valoresSorteados.Clear();
        }

        return novoValor;
    }

    void RugidoFalse()
    {
        rugido = false;
        canAct = true;
    }

    void actTrue()
    {
        canAct = true;
    }
    void EndDamaged()
    {
        endDamage = true;
    }
    void AttackAnimationComplete()
    {
        ataqueEmAndamento = false;
        actTrue();
    }
    void IdleCount()
    {
        idleCount = Random.Range(4, 6);
    }
    void NotVulnerable()
    {
        if (!vulnerabilityScheduled)
        {
            vulnerabilityScheduled = true;
            Invoke("SetNotVulnerable", 0.001f);
        }
    }

    void SetNotVulnerable()
    {
        AttackAnimationComplete();
        vulnerabilityScheduled = false;
        if (canAtkCount)
        {
            canAtkCount = false;
            attackCount = Random.Range(5, 8);
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    public void IniciarMudancaDeMaterial()
    {
        StartCoroutine(MudarMaterialTemporariamente());
    }
    private IEnumerator MudarMaterialTemporariamente()
    {
        AplicarNovoMaterial(core);
        AplicarNovoMaterial(spike1);
        AplicarNovoMaterial(spike2);
        AplicarNovoMaterial(spike3);
        AplicarNovoMaterial(rightArm);
        AplicarNovoMaterial(leftArm);

        yield return new WaitForSeconds(duracaoMudanca);

        RestaurarMaterialOriginal(core);
        RestaurarMaterialOriginal(spike1);
        RestaurarMaterialOriginal(spike2);
        RestaurarMaterialOriginal(spike3);
        RestaurarMaterialOriginal(rightArm);
        RestaurarMaterialOriginal(leftArm);
    }

    private void AplicarNovoMaterial(SpriteRenderer renderer)
    {
        renderer.material = novoMaterial;
    }
    private void RestaurarMaterialOriginal(SpriteRenderer renderer)
    {
        renderer.material = defaultMaterial;
    }
    void CanAttacking()
    {
        canAttack = true;
    }
    void CantAttack()
    {
        canAttack = false;
    }
    void ActiveCollider()
    {
        Spike1.enabled = true;
        Spike2.enabled = true;
        Spike3.enabled = true;
    }
    void DesactiveCollider()
    {
        Spike1.enabled = false;
        Spike2.enabled = false;
        Spike3.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "playerRange" || collision.gameObject.tag == "mediumPlayerRange")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "playerRange" || collision.gameObject.tag == "mediumPlayerRange")
        {
            inRange = false;
        }
    }
}
