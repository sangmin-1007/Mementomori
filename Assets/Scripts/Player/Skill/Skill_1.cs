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

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
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
        this.damage = damge;
        this.count += count;

        if (id == 0)
            CreatSkill();
    }
    //�ʱ�ȭ
    public void Init(SkillData data)
    {
        name = "Weapon " + data.skillId;
        transform.parent = Managers.GameSceneManager.Player.transform;
        transform.localPosition = Vector3.zero;

        id = data.skillId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < _spawnManager.pool.prefabs.Length; index++)
        {
            if(data.projectile == _spawnManager.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
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

            //�⺻ ������Ʈ ���� Ȱ�� -> ���ڶ� �� Ǯ���ϱ�
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = _spawnManager.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            //���� �� ��ġ �ʱ�ȭ
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            //-1�� ������ ����
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

        Transform bullet = _spawnManager.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);
        bullet.GetComponent<Skill_Bullet>().Init(damage, count, dir);
    }
}
