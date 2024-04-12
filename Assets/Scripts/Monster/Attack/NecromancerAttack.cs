using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAttack : MonoBehaviour
{
    Rigidbody2D player;

    Animator animator;

    float shootRange = 5f;
    float defaultSpeed;

    float attackTime;
    float defaultAttackTime = 2.8f;

    public bool isAttacking = false;

    MonsterMovement movement;
    protected PlayerStatsHandler Stats { get; private set; }

    private HealthSystem healthSystem;
    private HealthSystem playerHealthSystem;

    private void Awake()
    {
        player = Managers.GameSceneManager.Player.GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<MonsterMovement>();

        healthSystem = GetComponent<HealthSystem>();
        Stats = GetComponent<PlayerStatsHandler>();
    }

    private void Start()
    {
        defaultSpeed = movement.speed;
        attackTime = 0f;
    }

    private void Update()
    {
        if (!player)
            return;

        attackTime -= Time.deltaTime;

        if (isAttacking)
        {
            return;
        }

        if (shootRange < DistanceToTarget())
        {
            OnMove();
        }
        else if (attackTime < 0f)
        {
            attackTime = defaultAttackTime;
            OnShoot();
        }
    }

    float DistanceToTarget()
    {
        if (!player)
        {
            return 0f;
        }
        return Vector3.Distance(transform.position, player.position);
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        movement.speed = 0f;

        yield return new WaitForSeconds(defaultAttackTime);

        var energy = ObjectPool.GetObjectEnergy();
        var direction = (new Vector3(player.transform.position.x, player.transform.position.y) - transform.position).normalized;
            energy.transform.position = transform.position + direction * 0.5f;
        energy.EnergyDamage(Stats.allAttack);
        energy.Shoot(direction * 5f);

        isAttacking = false;
    }

    void OnShoot()
    {
        if (!player)
        {
            return;
        }

        StartCoroutine(AttackCoroutine());
    }

    void OnMove()
    {
        movement.speed = defaultSpeed;
    }
}
