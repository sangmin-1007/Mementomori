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
    [SerializeField] private string targetTag = "Player";

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
        //if (direction.x > 0)
            energy.transform.position = transform.position + direction * 0.5f;
        //else
            //energy.transform.position = new Vector3(transform.position.x + direction.normalized.x * 0.5f, transform.position.y + direction.normalized.y * 0.5f + 1f);
        //energy.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
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
