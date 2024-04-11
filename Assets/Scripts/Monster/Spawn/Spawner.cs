using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    static public int stage = 1;
    float stageTimer = 0f;
    float bossTimer = 10f;
    static public bool boss = false;

    public Transform[] spawnPoint;

    private SpawnManager _spawnManager;

    float spawnTimer = 0f;
    public static int count = 0;

    private void Start()
    {
        count = 0;
        stage = 1;
        boss = false;
        stageTimer = 0f;
        bossTimer = 10f;
        spawnPoint = GetComponentsInChildren<Transform>();
        if(SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "GameScene-LIK")
        {
            _spawnManager = Managers.GameSceneManager.MonsterSpawner.GetComponent<SpawnManager>();
        }
    }

    private void Update()
    {
        if (_spawnManager == null)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > 0.5f && count < 30)
        {
            spawnTimer = 0f;

            Spawn();
            count++;
            //Debug.Log($"증가 후 몬스터 수 : {count}");
        }

        stageTimer += Time.deltaTime;
        //bossTimer = Managers.GameManager.timer;

        if (stageTimer > 20f && boss == false)
        {
            SpawnBoss();
            bossTimer = 10f;
            boss = true;
        }

        if (boss)
        {
            stageTimer = 0f;
            bossTimer -= Time.deltaTime;
            //Debug.Log(bossTimer);
        }

        if (bossTimer <= 0f)
        {
            boss = false;
            bossTimer = 10f;
            Managers.GameManager.GameOver();
        }
    }

    private void SpawnBoss()
    {
        if (_spawnManager.pool == null)
        {
            return;
        }
        GameObject monster = _spawnManager.pool.Get(stage + 3);
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
