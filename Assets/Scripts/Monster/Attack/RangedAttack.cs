using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    Rigidbody2D player;

    Animator animator;

    public float shootRange = 5f;
    float defaultSpeed;

    float attackTime;

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
            attackTime = 2f;
            OnShoot();
        }
    }

    float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        movement.speed = 0f;
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }

    void OnShoot()
    {
        var arrow = ObjectPool.GetObject();
        var direction = new Vector3(player.transform.position.x, player.transform.position.y) - transform.position;
        arrow.transform.position = transform.position + direction.normalized * 0.5f;
        arrow.Shoot(direction.normalized * Time.deltaTime);

        StartCoroutine(AttackCoroutine());
    }

    void OnMove()
    {
        movement.speed = defaultSpeed;
    }
}
