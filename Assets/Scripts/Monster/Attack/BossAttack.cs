using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossAttack : MonoBehaviour
{
    Animator animator;

    bool isAttacking = false;
    float defaultSpeed;
    MonsterMovement movement;

    protected PlayerStatsHandler Stats { get; private set; }
    [SerializeField] private string targetTag = "Player";

    private HealthSystem playerHealthSystem;


    public SpriteRenderer spriteRenderer;

    Vector2 pos1;
    Vector2 pos2;
    Vector2 pos3;
    Vector2 boxSize1;
    Vector2 boxSize2;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<MonsterMovement>();
        Stats = GetComponent<PlayerStatsHandler>();
    }

    private void Start()
    {
        defaultSpeed = movement.speed;
    }

    private void Update()
    {
        pos1 = transform.position + new Vector3(2f, 0.17f);
        pos2 = transform.position + new Vector3(-1f, 2.5f);
        pos3 = transform.position + new Vector3(-1f, 0.17f);
        boxSize1 = new Vector3(3f, 5f);
        boxSize2 = new Vector3(2f, 2f);

        if (!isAttacking)
            OnMove();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isAttacking)
            return;

        if (collision.tag == targetTag)
        {
            playerHealthSystem = collision.GetComponent<HealthSystem>();

            StartCoroutine(OnContactAttack(collision));
        }
    }

    IEnumerator OnContactAttack(Collider2D collision)
    {
        if (Stats.CurrentStates.attackSO == null)
            yield break;

        isAttacking = true;
        movement.speed = 0f;
        animator.SetTrigger("Ready");

        yield return new WaitForSeconds(1f);

        animator.SetTrigger("Attack");

        OnShoot(Stats.CurrentStates.attackSO);

        //yield return new WaitForSeconds(1f);

        isAttacking = false;
    }

    void OnMove()
    {
        movement.speed = defaultSpeed;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos2, boxSize2);
        if (spriteRenderer.flipX == false)
        {
            Gizmos.DrawWireCube(pos1, boxSize1);
        }
        else if (spriteRenderer.flipX == true)
        {
            Gizmos.DrawWireCube(pos3, boxSize1);
        }
    }

    private void OnShoot(AttackSO attackSO)
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(pos2, boxSize2, 0);

        //피격 박스(위쪽) 데미지 계산
        foreach (Collider2D collider in collider2D)
        {
            if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
            {
                HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.ChangeHealth(-attackSO.power);
                    if (attackSO.isOnKnockback)
                    {
                        Movement movement = collider.GetComponent<Movement>();
                        if (movement != null)
                        {
                            movement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
                        }
                    }
                }
            }
        }

        //피격 박스(오른쪽) 데미지 계산
        if (spriteRenderer.flipX == false)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos1, boxSize1, 0);

            foreach (Collider2D collider in collider2Ds)
            {
                if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                {
                    HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                    if (healthSystem != null)
                    {
                        healthSystem.ChangeHealth(-attackSO.power);
                        if (attackSO.isOnKnockback)
                        {
                            Movement movement = collider.GetComponent<Movement>();
                            if (movement != null)
                            {
                                movement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
                            }
                        }
                    }
                }
            }
        }
        //피격 박스(왼쪽) 데미지 계산
        else if (spriteRenderer.flipX == true)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos3, boxSize1, 0);

            foreach (Collider2D collider in collider2Ds)
            {
                if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                {
                    HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                    if (healthSystem != null)
                    {
                        healthSystem.ChangeHealth(-attackSO.power);
                        if (attackSO.isOnKnockback)
                        {
                            Movement movement = collider.GetComponent<Movement>();
                            if (movement != null)
                            {
                                movement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
                            }
                        }
                    }
                }
            }
        }
    }
}
