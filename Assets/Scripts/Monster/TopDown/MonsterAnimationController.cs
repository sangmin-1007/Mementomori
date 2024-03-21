using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationController : MonsterAnimations
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        //Move.
    }

    private void Walk(Vector2 obj)
    {
        Animator.SetBool(IsWalking, obj.magnitude > .5f);
    }

    //private void Attacking(AttackSO obj)
    //{
    //    Animator.SetTrigger(Attack);
    //}

    private void Hit()
    {
        Animator.SetBool(IsHit, true);
    }

    private void InvincibilityEnd()
    {
        Animator.SetBool(IsHit, false);
    }
}
