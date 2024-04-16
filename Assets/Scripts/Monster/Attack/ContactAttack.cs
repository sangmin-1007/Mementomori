using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAttack : MonoBehaviour
{
    Animator animator;

    bool isAttacking = false;
    float defaultSpeed;
    MonsterMovement movement;

    protected PlayerStatsHandler Stats { get; private set; }
    [SerializeField] private string targetTag = "Player";

    private HealthSystem playerHealthSystem;
    private float CurrentDefense;
    private PlayerStatsHandler _statsHandler;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<MonsterMovement>();
        Stats = GetComponent<PlayerStatsHandler>();
        playerHealthSystem = GetComponent<HealthSystem>();
        _statsHandler = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
        CurrentDefense = _statsHandler.allDefense;
    }

    private void Start()
    {
        defaultSpeed = movement.speed;
    }

    private void Update()
    {
        if (!isAttacking)
            OnMove();
        CurrentDefense = _statsHandler.allDefense;
        _statsHandler.EquipStatApply();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isAttacking)
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
        bool hasBeenChanged = playerHealthSystem.ChangeHealth(-attackSO.power + (attackSO.power * CurrentDefense/100));

        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }

    void OnMove()
    {
        movement.speed = defaultSpeed;
    }
}
