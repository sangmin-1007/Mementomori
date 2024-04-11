using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    SkeletonCat,
    SkeletonWarrior,
    SkeletonArcher,
    Necromancer,
    Boss,
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
        TypeByAcquisitionGold(_monsterType);
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
        Spawner.count--;
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

    private void TypeByAcquisitionGold(MonsterType monsterType)
    {
        switch(monsterType)
        {
            case MonsterType.SkeletonCat:
                Managers.UserData.acquisitionGold += 1;
                break;
            case MonsterType.SkeletonWarrior:
                Managers.UserData.acquisitionGold += 5;
                break;
            case MonsterType.SkeletonArcher:
                Managers.UserData.acquisitionGold += 7;
                break;
            case MonsterType.Necromancer:
                Managers.UserData.acquisitionGold += 10;
                break;
            case MonsterType.Boss:
                Managers.UserData.acquisitionGold += 100;
                break;
            default:
                break;
        }
    }
}
