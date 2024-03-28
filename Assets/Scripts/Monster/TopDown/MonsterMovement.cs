using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    Rigidbody2D player;
    public float speed = 1f;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    RangedAttack rangedAttack;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rangedAttack = GetComponent<RangedAttack>();
    }

    void FixedUpdate()
    {
        if(player != null)
        {
            Movement();
            Rotation(player.transform.position.x - transform.position.x);
            if ((player.transform.position - transform.position).magnitude > 15)
                Reposition();
        }
    }

    private void Reposition()
    {
        transform.position += (player.transform.position - transform.position) * 1.6f;
    }

    void Movement()
    {
        // Player 추적
        Vector2 direction = player.position - rigid.position;
        rigid.position += direction.normalized * speed * Time.fixedDeltaTime;
        rigid.velocity = Vector2.zero;
    }

    void Rotation(float rot)
    {
        // 플레이어 x 좌표에 따라 방향 전환
        if (rot > 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;
    }

    private void OnEnable()
    {
        player = Managers.GameSceneManager.Player.GetComponent<Rigidbody2D>();
    }
}