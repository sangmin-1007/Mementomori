using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }
    public bool IsDashing { get; set; }
    protected PlayerStatsHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<PlayerStatsHandler>();
    }

    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (Stats.CurrentStates.attackSO == null)
            return;

        float attackDelay = 1 / Stats.CurrentStates.attackSO.delay;

        if (_timeSinceLastAttack <= attackDelay)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }
        if (IsAttacking && _timeSinceLastAttack > attackDelay)
        {
            _timeSinceLastAttack = 0f;
            CallAttackEvent(Stats.CurrentStates.attackSO);
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    public void CallAttackEvent(AttackSO attackSO)
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            OnAttackEvent?.Invoke(attackSO);
        }
       
    }

 

}
