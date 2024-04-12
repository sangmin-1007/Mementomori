using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputController : PlayerController
{
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    //샌드메세지방식 실행되었을때 돌려받는 함수를 만드는것
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnDash(InputValue value)
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            IsDashing = value.isPressed;
        }
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);

        newAim = (worldPos - (Vector2)transform.position).normalized;
        CallLookEvent(newAim);
    }

    public void OnAttack(InputValue value)
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if(Managers.UI_Manager.IsActive<UI_Skill>())
            {
                IsAttacking = false;
            }
            else
            {
                IsAttacking = value.isPressed;
            }
            
        }
    }

    public void OnInventory(InputValue value)
    {
        if (!Managers.UI_Manager.IsActive<UI_Storage>() && !Managers.UI_Manager.IsActive<UI_Shop>())
        {
            if (value.isPressed)
            {
                if (!Managers.UI_Manager.IsActive<UI_Inventory>())
                {
                    Managers.UI_Manager.ShowUI<UI_Inventory>();
                }
                else
                {
                    Managers.UI_Manager.HideUI<UI_Inventory>();
                }
            }
        }
       
    }


    public void OnOption(InputValue value)
    {
        if (value.isPressed)
        {
            if (!Managers.UI_Manager.IsActive<UI_Option>())
            {
                Managers.UI_Manager.ShowUI<UI_Option>();
            }
            else
            {
                Managers.UI_Manager.HideUI<UI_Option>();
            }
        }
    }

    public void OnStat(InputValue value)
    {
        if(value.isPressed)
        {
            if (!Managers.UI_Manager.IsActive<UI_Stats>())
            {
                Managers.UI_Manager.ShowUI<UI_Stats>();
            }
            else
            {
                Managers.UI_Manager.HideUI<UI_Stats>();
            }
        }
    }
}
