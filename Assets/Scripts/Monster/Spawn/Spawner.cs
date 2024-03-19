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
    public static int count = 0;

    private void Start()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        _spawnManager = Managers.GameSceneManager.MonsterSpawner.GetComponent<SpawnManager>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f && count < 10)
        {
            timer = 0f;
            if (_spawnManager.pool == null)
            {
                return;
            }
            Spawn();
            count++;
            //Debug.Log($"증가 후 몬스터 수 : {count}");
        }
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
