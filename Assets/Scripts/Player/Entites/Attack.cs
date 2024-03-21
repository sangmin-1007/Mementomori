using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public GameObject pos_1;
    public Transform pos2;
    public Transform pos3;
    public GameObject pos_3;
    public Vector2 boxSize1;
    public Vector2 boxSize2;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(pos2.position, boxSize2);
        if (spriteRenderer.flipX == false)
        {
            Gizmos.DrawWireCube(pos1.position, boxSize1);
        }
        else if (spriteRenderer.flipX == true)
        {
            Gizmos.DrawWireCube(pos3.position, boxSize1);
        }
    }

    private void OnShoot(AttackSO attackSO)
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(pos2.position, boxSize2, 0);

        //피격 박스(위쪽) 데미지 계산
        foreach (Collider2D collider in collider2D)
        {
            if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
            {
                HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.ChangeHealth(-attackSO.power);
                    if (attackSO.isOnKnockback)
                    {
                        Movement movement = collider.GetComponent<Movement>();
                        if (movement != null)
                        {
                            movement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
                        }
                    }
                }
            }
        }

        //피격 박스(오른쪽) 데미지 계산
        if (spriteRenderer.flipX == false)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos1.position, boxSize1, 0);
            
            foreach (Collider2D collider in collider2Ds)
            {
                if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                {
                    HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                    if(healthSystem != null )
                    {
                        healthSystem.ChangeHealth(-attackSO.power);
                        if(attackSO.isOnKnockback)
                        {
                            Movement movement = collider.GetComponent<Movement>();
                            if(movement != null )
                            {
                                movement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
                            }
                        }
                    }
                }
            }
        }
        //피격 박스(왼쪽) 데미지 계산
        else if (spriteRenderer.flipX == true)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos3.position, boxSize1, 0);

            foreach (Collider2D collider in collider2Ds)
            {
                if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
                {
                    HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                    if (healthSystem != null)
                    {
                        healthSystem.ChangeHealth(-attackSO.power);
                        if (attackSO.isOnKnockback)
                        {
                            Movement movement = collider.GetComponent<Movement>();
                            if (movement != null)
                            {
                                movement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
                            }
                        }
                    }
                }
            }
        }

       
    }
}
