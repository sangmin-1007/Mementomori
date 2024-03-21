using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private PlayerController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        
    }
}
