using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    //센드메세지방식 실행되었을때 돌려받는 함수를 만드는것
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if(newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnAttack(InputValue value)
    {

    }
}
