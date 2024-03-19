using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerHealthController : PlayerController
{
    //[SerializeField] private string targetTag = "Player";
    private bool _isCollidingWithTarget;

    private HealthSystem healthSystem;
    private HealthSystem _collidingTargetHealthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void FixedUpdate()
    {
        if (_isCollidingWithTarget)
        {
            ApplyHealthChange();
        }
    }

    private void ApplyHealthChange()
    {
        AttackSO attackSO = Stats.CurrentStates.attackSO;
        bool hasBeenChanged = _collidingTargetHealthSystem.ChangeHealth(-attackSO.power);
        //if (attackSO.isOnKnockback && _collidingMovement != null)
        //{
        //    _collidingMovement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
        //}
    }
}
