using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAttack : MonoBehaviour
{
    Animator animator;
    AttackSO attackSO;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attackSO = GetComponent<AttackSO>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnContactAttack(attackSO, collision);
    }

    void OnContactAttack(AttackSO attackSO, Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetTrigger("Attack");

            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-attackSO.power);
                //if (attackSO.isOnKnockback)
                //{
                //    Movement movement = collision.GetComponent<Movement>();
                //    if (movement != null)
                //    {
                //        movement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
                //    }
                //}
            }
        }
    }
}
