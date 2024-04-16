using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_1 : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
 
    private SpawnManager _spawnManager;
    private Scanner _scanner;

    private void Awake()
    {
        _spawnManager = Managers.GameSceneManager.MonsterSpawner.GetComponent<SpawnManager>();
        _scanner = Managers.GameSceneManager.Player.GetComponent<Scanner>();
    }

    void Update()
    {
        switch (id)
        {
            case 5:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    public void LevelUp(float damge,int count)
    {
        this.damage += damge;
        this.count += count;

        if (id == 5)
            CreatSkill();
    }

    public void Init(SkillData1 data)
    {
        name = "Weapon " + data.skillId;
        transform.parent = Managers.GameSceneManager.Player.transform;
        transform.localPosition = Vector3.zero;

        id = data.skillId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < _spawnManager.pool.skillPrefabs.Length; index++)
        {
            if(data.projectile == _spawnManager.pool.skillPrefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 5:
                speed = 150;
                CreatSkill();
                break;
            default:
                speed = 1f;
                break;
        }
    }

    void CreatSkill()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = _spawnManager.pool.SkillGet(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Skill_Bullet>().Init(damage,-1,Vector3.zero); 
        }
    }
    private void Fire()
    {
        if(!_scanner.nearestTarget)
            return;

        Vector3 targetPos = _scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = _spawnManager.pool.SkillGet(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);
        bullet.GetComponent<Skill_Bullet>().Init(damage, count, dir);
    }
}
