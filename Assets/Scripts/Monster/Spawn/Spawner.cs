using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            timer = 0f;
            if (SpawnManager.instance.pool == null)
            {
                return;
            }
            Spawn();
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
