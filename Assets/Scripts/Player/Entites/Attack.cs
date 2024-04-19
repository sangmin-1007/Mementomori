using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private PlayerStatsHandler _statsHandler;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private PlayerController _controller;
    private Vector2 _aimDirection = Vector2.right;

    private UI_Skill _Skill;
    private SkillManager[] skills;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _statsHandler = GetComponent<PlayerStatsHandler>();
        _Skill = Managers.UI_Manager.ShowUI<UI_Skill>();
        Managers.UI_Manager.HideUI<UI_Skill>();
        skills = _Skill.skills;
    }

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

        if(!_controller.IsDashing)
        {
            Managers.SoundManager.Play("Effect/PlayerAttackFail1", Sound.Effect);
        }


        foreach (Collider2D collider in collider2D)
        {
            if (attackSO.target.value == (attackSO.target.value | (1 << collider.gameObject.layer)))
            {
                HealthSystem healthSystem = collider.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.ChangeHealth(-_statsHandler.allAttack);
                }
            }
            if (skills[6].skillLevel == 0)
                return;
            else if (skills[6].skillLevel != 0)
            {
                Managers.GameSceneManager.Player.GetComponent<HealthSystem>().ChangeHealth(_statsHandler.allAttack * _Skill.skills[6].data.damages[_Skill.skills[6].skillLevel]);
            }
        }

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
                        healthSystem.ChangeHealth(-_statsHandler.allAttack);
                    }
                }
                if (skills[6].skillLevel == 0)
                    return;
                else if (skills[6].skillLevel != 0)
                {
                    Managers.GameSceneManager.Player.GetComponent<HealthSystem>().ChangeHealth(_statsHandler.allAttack * _Skill.skills[6].data.damages[_Skill.skills[6].skillLevel]);
                }
            }
        }

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
                        healthSystem.ChangeHealth(-_statsHandler.allAttack);
                    }
                }
                if (skills[6].skillLevel == 0)
                    return;
                else if (skills[6].skillLevel != 0)
                {
                    Managers.GameSceneManager.Player.GetComponent<HealthSystem>().ChangeHealth(_statsHandler.allAttack * _Skill.skills[6].data.damages[_Skill.skills[6].skillLevel]);
                }
            }
        }

       
    }
}
