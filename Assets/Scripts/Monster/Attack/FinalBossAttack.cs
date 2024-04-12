using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAttack : MonoBehaviour
{
    [SerializeField] private AttackSO attackSO;
    PlayerStatsHandler Stats;
    HealthSystem monsterHealthSystem;
    HealthSystem playerHealthSystem;

    int phase;

    Animator animator;
    bool isAttacking = false;
    float defaultSpeed;
    MonsterMovement movement;
    string targetTag = "Player";

    public SpriteRenderer spriteRenderer;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Vector2 boxSize1;
    public Vector2 boxSize2;

    public Transform pos4;
    public Transform pos5;
    public Transform pos6;
    public Vector2 boxSize4;
    public Vector2 boxSize5;

    public Transform pos7;
    public Transform pos8;
    public Transform pos9;
    public Vector2 boxSize7;
    public Vector2 boxSize8;

    private void Awake()
    {
        Stats = GetComponent<PlayerStatsHandler>();
        monsterHealthSystem = GetComponent<HealthSystem>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<MonsterMovement>();
    }

    void Start()
    {
        defaultSpeed = movement.speed;
        phase = 1;
    }

    void Update()
    {
        if (monsterHealthSystem.CurrentHealth / Stats.CurrentStates.maxHealth < 0.3f)
            phase = 3;
        else if (monsterHealthSystem.CurrentHealth / Stats.CurrentStates.maxHealth < 0.6f)
            phase = 2;
        else
            phase = 1;

        Debug.Log(monsterHealthSystem.CurrentHealth / Stats.CurrentStates.maxHealth);

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

        switch (phase)
        {
            case 1:
                animator.SetTrigger("Attack");
                break;
            case 2:
                int randomIndex = Random.Range(0, 3);
                RandomAttack(randomIndex);

                break;
            case 3:
                int randomIndex2 = Random.Range(0, 4);
                RandomAttack(randomIndex2);
                break;
            default:
                break;
        }

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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(pos5.position, boxSize5);
        if (spriteRenderer.flipX == false)
        {
            Gizmos.DrawWireCube(pos4.position, boxSize4);
        }
        else if (spriteRenderer.flipX == true)
        {
            Gizmos.DrawWireCube(pos6.position, boxSize4);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos8.position, boxSize8);
        if (spriteRenderer.flipX == false)
        {
            Gizmos.DrawWireCube(pos7.position, boxSize7);
        }
        else if (spriteRenderer.flipX == true)
        {
            Gizmos.DrawWireCube(pos9.position, boxSize7);
        }
    }

    private void OnShoot(int attackType)
    {
        switch (attackType)
        {
            case 1:
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(pos2.position, boxSize2, 0);

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
                break;

            case 2:
                Collider2D[] collider2D2 = Physics2D.OverlapBoxAll(pos5.position, boxSize5, 0);

                foreach (Collider2D collider in collider2D2)
                {
                    if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                    {
                        HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                        if (healthSystem != null)
                        {
                            healthSystem.ChangeHealth(-attackSO.power*2);
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

                if (spriteRenderer.flipX == false)
                {
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos4.position, boxSize4, 0);

                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                        {
                            HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                            if (healthSystem != null)
                            {
                                healthSystem.ChangeHealth(-attackSO.power*2);
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
                else if (spriteRenderer.flipX == true)
                {
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos6.position, boxSize4, 0);

                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                        {
                            HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                            if (healthSystem != null)
                            {
                                healthSystem.ChangeHealth(-attackSO.power * 2);
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
                break;

            case 3:
                Collider2D[] collider2D3 = Physics2D.OverlapBoxAll(pos8.position, boxSize8, 0);

                foreach (Collider2D collider in collider2D3)
                {
                    if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                    {
                        HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                        if (healthSystem != null)
                        {
                            healthSystem.ChangeHealth(-attackSO.power * 3);
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

                if (spriteRenderer.flipX == false)
                {
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos7.position, boxSize7, 0);

                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                        {
                            HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                            if (healthSystem != null)
                            {
                                healthSystem.ChangeHealth(-attackSO.power * 3);
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
                else if (spriteRenderer.flipX == true)
                {
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos9.position, boxSize7, 0);

                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                        {
                            HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                            if (healthSystem != null)
                            {
                                healthSystem.ChangeHealth(-attackSO.power * 3);
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
                break;
            default:
                break;
        }
    }

    private void RandomAttack(int index)
    {
        switch(index)
        {
            case 0:
                animator.SetTrigger("Attack");
                break;
            case 1:
                animator.SetTrigger("Attack2");
                break;
            case 2:
                animator.SetTrigger("Attack12");
                break;
            case 3:
                animator.SetTrigger("Attack3");
                break;
            default:
                break;
        }
    }
}
