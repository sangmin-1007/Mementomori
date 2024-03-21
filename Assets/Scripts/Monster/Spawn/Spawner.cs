using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public int stage = 1;

    public Transform[] spawnPoint;

    private SpawnManager _spawnManager;

    float timer = 0f;
    //float stage_time = 0f;
    public static int count = 0;

    private void Start()
    {
        count = 0;

        spawnPoint = GetComponentsInChildren<Transform>();
        if(SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "GameScene-LIK")
        {
            _spawnManager = Managers.GameSceneManager.MonsterSpawner.GetComponent<SpawnManager>();
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
        //stage_time += Time.deltaTime;

        //if(stage_time > 30f)
        //{
        //    SpawnBoss();
        //    return;
        //}

        if (_spawnManager == null)
            return;

        if (timer > 1f && count < 10)
        {
            timer = 0f;

            Spawn();
            count++;
            //Debug.Log($"���� �� ���� �� : {count}");
        }

    }

    private void SpawnBoss()
    {
        
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
