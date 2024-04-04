using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Normal,
    Boss
}

public class MonsterDeath : MonoBehaviour
{
    PlayerController _controller;
    HealthSystem _healthSystem;
    Collider2D coll;
    Rigidbody2D rigid;

    [SerializeField] private MonsterType _monsterType;

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
        Managers.ItemObjectPool.SpawnItem(transform.position, 50001000);
        if(_monsterType == MonsterType.Boss)
        {
            Managers.ItemObjectPool.SpawnItem(transform.position, DataBase.Item.GetRandomItemID());
        }


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
        Managers.SoundManager.Play("Effect/Monster_Die1", Sound.Effect);
        Invoke("SetDie", 1f);
        //coll.enabled = false;
        //rigid.simulated = false;
        Spawner.count--;
        //Debug.Log($"감소 후 몬스터 수 : {Spawner.count}");
    }

    void SetDie()
    {
        
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = true;
        }
        gameObject.SetActive(false);
    }
}
