using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossAttack : MonoBehaviour
{
    BossAttack bossAttack;

    Animator animator;

    bool isAttacking = false;
    float defaultSpeed;
    MonsterMovement movement;

    protected PlayerStatsHandler Stats { get; private set; }
    [SerializeField] private string targetTag = "Player";

    private HealthSystem playerHealthSystem;
    private PlayerStatsHandler playerStats;

    private float CurrentDefense;

    public SpriteRenderer spriteRenderer;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Vector2 boxSize1;
    public Vector2 boxSize2;

    private void Awake()
    {
        bossAttack = GetComponent<BossAttack>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<MonsterMovement>();
        Stats = GetComponent<PlayerStatsHandler>();
        playerStats = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();

    }

    private void Start()
    {
        defaultSpeed = movement.speed;
    }

    private void Update()
    {
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

        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }

    void OnMove()
    {
        movement.speed = defaultSpeed;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos2.position, boxSize2);
        if (spriteRenderer.flipX == false)
        {
            Gizmos.DrawWireCube(pos1.position, boxSize1);
        }
        else if (spriteRenderer.flipX == true)
        {
            Gizmos.DrawWireCube(pos3.position, boxSize1);
        }
    }

    private void OnShoot(AttackSO attackSO)
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(pos2.position, boxSize2, 0);

        CurrentDefense = playerStats.allDefense;

        foreach (Collider2D collider in collider2D)
        {
            if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
            {
                HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.ChangeHealth(-attackSO.power + (attackSO.power * CurrentDefense / 100));
                }
            }
        }

        if (spriteRenderer.flipX == false)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos1.position, boxSize1, 0);

            foreach (Collider2D collider in collider2Ds)
            {
                if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                {
                    HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                    if (healthSystem != null)
                    {
                        healthSystem.ChangeHealth(-attackSO.power + (attackSO.power * CurrentDefense / 100));
                    }
                }
            }
        }

        else if (spriteRenderer.flipX == true)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos3.position, boxSize1, 0);

            foreach (Collider2D collider in collider2Ds)
            {
                if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                {
                    HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                    if (healthSystem != null)
                    {
                        healthSystem.ChangeHealth(-attackSO.power + (attackSO.power * CurrentDefense / 100));
                    }
                }
            }
        }
    }
}
