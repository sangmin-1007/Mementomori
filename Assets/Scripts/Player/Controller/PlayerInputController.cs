using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    //����޼������ ����Ǿ����� �����޴� �Լ��� ����°�
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnDash(InputValue value)
    {
        IsDashing = value.isPressed;
        

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
        IsAttacking = value.isPressed;
    }

    public void OnInventory(InputValue value)
    {
        if (value.isPressed)
        {
            if(!Managers.UI_Manager.IsActive<Inventory>())
            {
                Managers.UI_Manager.ShowUI<Inventory>();
            }
            else
            {
                Managers.UI_Manager.HideUI<Inventory>();
            }
        }
    }
}
