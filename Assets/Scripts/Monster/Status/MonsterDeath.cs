using Constants;
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
        //Managers.ItemObjectPool.SpawnItem(transform.position, 50001000);

        #region 아이템 드롭 테스트
        int dropRate = UnityEngine.Random.Range(1, 101);
        if (dropRate <= 50)
        {
            Managers.ItemObjectPool.SpawnItem(transform.position, 50001000);
        }
        else
        {
            Managers.ItemObjectPool.SpawnItem(transform.position, DataBase.Item.GetRandomItemID());
        }
        #endregion

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
