using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        anim = GetComponent<Animator>();
        Spike1.enabled = false;
        Spike2.enabled = false;
        Spike3.enabled = false;
    }

    void Update()
    {
        if (attackCount > 0)
        {
            animCore.Play("boss_Core_Idle");
        }
        if (rugido)
        {
            ChangeAnimationState(RUGIDO_BOSS);
            podeAgir = true;
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

            canAct = false;
            Invoke("actTrue", anim.GetCurrentAnimatorStateInfo(0).length);
        }
        if (attackCount <= 0)
        {
            canAct = false;
            if (playerMove.playerAttack && inRange)
            {
                IniciarMudancaDeMaterial();
            }
            ChangeAnimationState(BOSS_VULNERABLE);
            animCore.Play("boss_Core_Open");
        }
        if(attackCount <= 0 && idleCount <= 0)
        {
            idleCount = 1;
        }
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
