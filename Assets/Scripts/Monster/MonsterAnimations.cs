using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimations : MonoBehaviour
{
    protected Animator Animator;
    protected MonsterMovement Move;

    protected virtual void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Move = GetComponent<MonsterMovement>();
    }
}
