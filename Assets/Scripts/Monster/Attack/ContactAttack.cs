using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAttack : MonoBehaviour
{
    Animator animator;

    protected PlayerStatsHandler Stats { get; private set; }
    [SerializeField] private string targetTag = "Player";

    private HealthSystem healthSystem;
    private HealthSystem playerHealthSystem;
    //private TopDownMovement _collidingMovement;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        healthSystem = GetComponent<HealthSystem>();
        Stats = GetComponent<PlayerStatsHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            playerHealthSystem = collision.GetComponent<HealthSystem>();
            OnContactAttack(collision);
        }
    }

    void OnContactAttack(Collider2D collision)
    {
        if (Stats.CurrentStates.attackSO == null)
            return;

        animator.SetTrigger("Attack");

        AttackSO attackSO = Stats.CurrentStates.attackSO;
        bool hasBeenChanged = playerHealthSystem.ChangeHealth(-attackSO.power);
    }
}
