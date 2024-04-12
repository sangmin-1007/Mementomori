using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 direction;

    string targetTag = "Player";
    HealthSystem playerHealthSystem;
    private PlayerStatsHandler playerStats;
    public PlayerStatsHandler Stats { get; private set; }

    private float damage;

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        Invoke("DestroyArrow", 5f);
    }

    public void DestroyArrow()
    {
        ObjectPool.ReturnObjectArrow(this);
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime);
    }

    private void OnEnable()
    {
        Stats = GetComponent<PlayerStatsHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == targetTag)
        {
            playerHealthSystem = collision.GetComponent<HealthSystem>();
            playerStats = collision.GetComponent<PlayerStatsHandler>();
            if (Stats.CurrentStates.attackSO == null)
                return;
            float blockDamage = damage - (damage * playerStats.allDefense / 100);
            playerHealthSystem.ChangeHealth(-blockDamage);

            DestroyArrow();
        }
    }

    public void ArrowDamage(float monsterDamage)
    {
        damage = monsterDamage;
    }
}
