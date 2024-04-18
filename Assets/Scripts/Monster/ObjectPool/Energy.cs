using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private Vector3 direction;

    string targetTag = "Player";
    HealthSystem playerHealthSystem;
    private PlayerStatsHandler playerStats;
    public PlayerStatsHandler Stats { get; private set; }
    private float damage;

    Animator animator;

    Vector3 moving;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        moving = transform.position;
    }

    public void DestroyEnergy()
    {
        ObjectPool.ReturnObjectEnergy(this);
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime);
        if((moving - transform.position).magnitude > 3f)
        {
            DestroyEnergy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            playerHealthSystem = collision.GetComponent<HealthSystem>();
            playerStats = collision.GetComponent<PlayerStatsHandler>();

            float blockDamage = damage - (damage * playerStats.allDefense / 100);

            bool hasBeenChanged = playerHealthSystem.ChangeHealth(-blockDamage);
            animator.SetTrigger("IsHit");
            direction = Vector3.zero;

            Invoke("DestroyEnergy", 0.8f);
        }
    }

    private void OnEnable()
    {
        Stats = GetComponent<PlayerStatsHandler>();
    }

    public void EnergyDamage(float monsterDamage)
    {
        damage = monsterDamage;
    }
}
