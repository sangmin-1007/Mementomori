using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Rigidbody2D player;
    public float speed = 3f;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
        Vector2 direction = player.position - rigid.position;
        rigid.position += direction.normalized * speed * Time.fixedDeltaTime;
        rigid.velocity = Vector2.zero;
    }

    void Rotation(float rot)
    {
        if (rot < 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;
    }

    private void OnEnable()
    {
        player = Managers.GameSceneManager.Player.GetComponent<Rigidbody2D>();
    }
}