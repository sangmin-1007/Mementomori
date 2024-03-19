using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeath : MonoBehaviour
{
    PlayerController _controller;
    HealthSystem _healthSystem;
    Collider2D coll;
    Rigidbody2D rigid;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _healthSystem = GetComponent<HealthSystem>();
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        _healthSystem.OnDeath += OnDie;
    }

    private void OnDie()
    {
        rigid.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        gameObject.SetActive(false);
        //coll.enabled = false;
        //rigid.simulated = false;
        Spawner.count--;
        Debug.Log($"감소 후 몬스터 수 : {Spawner.count}");
    }
}
