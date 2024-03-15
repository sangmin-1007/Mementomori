using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    Rigidbody2D player;
    [SerializeField] float speed = 1f;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Movement();
        Rotation(player.transform.position.x - transform.position.x);
        Reposition();
    }

    private void Reposition()
    {
        if ((player.transform.position - transform.position).magnitude > 15)
            transform.position += (player.transform.position - transform.position) * 2f;
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
        player = SpawnManager.instance.player.GetComponent<Rigidbody2D>();
    }
}