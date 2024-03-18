using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeath : MonoBehaviour
{
    PlayerController _controller;
    HealthSystem _healthSystem;
    Collider2D coll;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _healthSystem = GetComponent<HealthSystem>();
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
            return;

        OnDie();
    }

    private void OnDie()
    {
        gameObject.SetActive(false);
        Spawner.count--;
    }
}
