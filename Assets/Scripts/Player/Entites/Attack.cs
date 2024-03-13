using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private PlayerController _controller;
    private Vector2 _aimDirection = Vector2.right;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller.OnAttackEvent += OnShoot;
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection;
    }

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Vector2 boxSize1;
    public Vector2 boxSize2;

    private void OnShoot(AttackSO attackSO)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos1.position,boxSize1,0);

        foreach (Collider2D collider in collider2Ds)
        {
            Debug.Log(collider.tag);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(pos2.position, boxSize2);
        if (spriteRenderer.flipX == false)
        {
            Gizmos.DrawWireCube(pos1.position, boxSize1);
        }
        else
        {
            Gizmos.DrawWireCube(pos3.position, boxSize1);
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
