using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 direction;

    string targetTag = "Player";
    HealthSystem playerHealthSystem;
    protected PlayerStatsHandler Stats { get; private set; }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == targetTag)
        {
            playerHealthSystem = collision.GetComponent<HealthSystem>();
            if (Stats.CurrentStates.attackSO == null)
                return;
            AttackSO attackSO = Stats.CurrentStates.attackSO;
            bool hasBeenChanged = playerHealthSystem.ChangeHealth(-attackSO.power);

            DestroyArrow();
        }
    }

    private void OnEnable()
    {
        Stats = GetComponent<PlayerStatsHandler>();
    }
}
