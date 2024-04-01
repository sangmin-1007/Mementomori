using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public Animator animator;
    private static readonly int IsDash = Animator.StringToHash("IsDash");

    private PlayerController _controller;
    private PlayerStatsHandler _stats;
    private HealthSystem _healthSystem;

    private Vector2 _movementDirction = Vector2.zero;
    private Rigidbody2D _rigidbody2D;

    private Vector2 _knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    
    public float CurrentStamina { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _stats = GetComponent<PlayerStatsHandler>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        defaultSpeed = _stats.CurrentStates.speed;
        CurrentStamina = _stats.CurrentStates.maxStamina;
        //±¸µ¶
        _controller.OnMoveEvent += Move;
    }
    private void Update()
    {
        Dash();
    }
    private void FixedUpdate()
    {
        ApplyMovement(_movementDirction);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    private void Move(Vector2 direction)
    {
        _movementDirction = direction;
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        _knockback = -(other.position - transform.position).normalized * power;
    }

    private float defaultSpeed;
    private bool isDash;
    public float dashSpeed;
    public float defaultTime;
    private float dashTime;

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * defaultSpeed;
        if(knockbackDuration > 0.0f)
        {
            direction += _knockback;
        }
        _rigidbody2D.velocity = direction;
    }
    private IEnumerator TriggerCourtine()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f); 
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield break;
    }
    private IEnumerator DashCourtine()
    {
        defaultSpeed = dashSpeed;
        yield return new WaitForSeconds(0.1f);
        defaultSpeed = _stats.CurrentStates.speed;
        yield break ;
    }
    private void Dash()
    {
       
        if (_controller.IsDashing && _healthSystem.GetCurrentSP() > 25)
        {
            Managers.SoundManager.Play("Effect/PlayerDash1", Sound.Effect);
            StartCoroutine(TriggerCourtine());
            animator.SetTrigger(IsDash);
            StartCoroutine(DashCourtine());
            _healthSystem.DecreaseStamina(25);
            _controller.IsDashing = false;
        }
    }
}
