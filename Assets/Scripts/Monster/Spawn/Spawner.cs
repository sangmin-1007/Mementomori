using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public int stage = 1;
    float bossTimer;

    public Transform[] spawnPoint;

    private SpawnManager _spawnManager;

    float timer = 0f;
    public static int count = 0;

    private void Start()
    {
        count = 0;

        stage = 4;

        spawnPoint = GetComponentsInChildren<Transform>();
        if(SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "GameScene-LIK")
        {
            _spawnManager = Managers.GameSceneManager.MonsterSpawner.GetComponent<SpawnManager>();
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;

        bossTimer = Managers.GameManager.timer;

        if (bossTimer > 30f)
        {
            SpawnBoss();

            bossTimer = 0f;
        }

        if (_spawnManager == null)
            return;

        if (timer > 0.5f && count < 10)
        {
            timer = 0f;

            Spawn();
            count++;
            //Debug.Log($"���� �� ���� �� : {count}");
        }

    }

    private void SpawnBoss()
    {
        if (_spawnManager.pool == null)
        {
            return;
        }
        GameObject monster = _spawnManager.pool.Get(4);
        monster.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }

    void Spawn()
    {
        if (_spawnManager.pool == null)
        {
            return;
        }
        GameObject monster = _spawnManager.pool.Get(Random.Range(0, stage));
        monster.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}
