using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerAttack : MonoBehaviour
{
    Animator animator;

    bool isAttacking = false;
    float defaultSpeed;
    BossMovement movement;

    protected PlayerStatsHandler Stats { get; private set; }
    [SerializeField] private string targetTag = "Player";

    private HealthSystem playerHealthSystem;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<BossMovement>();
        Stats = GetComponent<PlayerStatsHandler>();
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
        animator.SetTrigger("Attack");
        movement.speed = 0f;

        AttackSO attackSO = Stats.CurrentStates.attackSO;
        bool hasBeenChanged = playerHealthSystem.ChangeHealth(-attackSO.power);
        //Managers.SoundManager.Play("Effect/PlayerAttackFail1", Sound.Effect);

        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }

    void OnMove()
    {
        movement.speed = defaultSpeed;
    }
}
