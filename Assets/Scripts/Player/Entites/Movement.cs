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

    private Vector2 _movementDirction = Vector2.zero;
    private Rigidbody2D _rigidbody2D;

    private Vector2 _knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        defaultSpeed = speed;
    }

    private void Start()
    {
        //����
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

    public float speed;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDash = true;
            StartCoroutine(TriggerCourtine());
            animator.SetTrigger(IsDash);
        }
        if (dashTime <= 0f)
        {
            defaultSpeed = speed;
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
