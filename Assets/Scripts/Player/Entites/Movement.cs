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

    public float CurrentSpeed { get; private set; }
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
        CurrentSpeed = _stats.CurrentStates.speed;
        CurrentSpeed = defaultSpeed;
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
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(0.5f); 
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        yield break;
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _healthSystem.GetCurrentSP() > 25)
        {
            isDash = true;
            StartCoroutine(TriggerCourtine());
            animator.SetTrigger(IsDash);
            _healthSystem.DecreaseStamina(25);
        }
        if (dashTime <= 0f)
        {
            defaultSpeed = _stats.CurrentStates.speed;
            if (isDash)
                dashTime = defaultTime;
        }
        else
        {
            dashTime -= Time.deltaTime;
            defaultSpeed = dashSpeed;
        }
        isDash = false;
    }
}
