using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private Vector3 direction;

    string targetTag = "Player";
    HealthSystem playerHealthSystem;
    protected PlayerStatsHandler Stats { get; private set; }

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        Invoke("DestroyEnergy", 5f);
    }

    public void DestroyEnergy()
    {
        ObjectPool.ReturnObjectEnergy(this);
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            playerHealthSystem = collision.GetComponent<HealthSystem>();
            if (Stats.CurrentStates.attackSO == null)
                return;
            AttackSO attackSO = Stats.CurrentStates.attackSO;
            bool hasBeenChanged = playerHealthSystem.ChangeHealth(-attackSO.power);
            animator.SetTrigger("IsHit");
            direction = Vector3.zero;

            Invoke("DestroyEnergy", 0.8f);
        }
    }

    private void OnEnable()
    {
        Stats = GetComponent<PlayerStatsHandler>();
    }
}