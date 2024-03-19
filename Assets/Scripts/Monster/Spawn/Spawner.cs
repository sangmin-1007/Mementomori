using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    float timer = 0f;
    public static int count = 0;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f && count < 5)
        {
            timer = 0f;
            if (SpawnManager.instance.pool == null)
            {
                return;
            }
            Spawn();
            count++;
            Debug.Log($"증가 후 몬스터 수 : {count}");
        }
    }

    void Spawn()
    {
        if (SpawnManager.instance.pool == null)
        {
            return;
        }
        GameObject monster = SpawnManager.instance.pool.Get(Random.Range(0, 4));
        monster.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}
