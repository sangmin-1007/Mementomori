using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    static public int stage;
    float stageTimer;
    float bossTimer;
    static public bool boss;

    public Transform[] spawnPoint;

    private SpawnManager _spawnManager;

    float spawnTimer;
    public static int count;

    HealthSystem player;

    public GameObject Dragonewt;
    public GameObject Germud;
    public GameObject CarcassesCollector;
    public GameObject Ifrit;

    private void Start()
    {
        count = 0;
        stage = 1;
        boss = false;
        stageTimer = 0f;
        bossTimer = 60f;
        spawnTimer = 0f;
        spawnPoint = GetComponentsInChildren<Transform>();
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            _spawnManager = Managers.GameSceneManager.MonsterSpawner.GetComponent<SpawnManager>();
        }
        player = GetComponentInParent<HealthSystem>();
    }

    private void Update()
    {
        if (_spawnManager == null)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > 0.5f && count < 70)
        {
            spawnTimer = 0f;

            Spawn();
            count++;
        }

        stageTimer += Time.deltaTime;

        if (stageTimer > 60f && boss == false)
        {
            SpawnBoss();
            bossTimer = 60f;
            boss = true;
        }

        if (boss)
        {
            stageTimer = 0f;
            bossTimer -= Time.deltaTime;
        }

        if (bossTimer <= 0f)
        {
            boss = false;
            bossTimer = 60;
            player.ChangeHealth(-10000f);
        }
    }

    private void SpawnBoss()
    {


        if (_spawnManager.pool == null)
        {
            return;
        }

        switch (stage)
        {
            case 1:
                Instantiate(Dragonewt, spawnPoint[Random.Range(1, spawnPoint.Length)]);
                break;
            case 2:
                Instantiate(Germud, spawnPoint[Random.Range(1, spawnPoint.Length)]);
                break;
            case 3:
                Instantiate(CarcassesCollector, spawnPoint[Random.Range(1, spawnPoint.Length)]);
                break;
            case 4:
                Instantiate(Ifrit, spawnPoint[Random.Range(1, spawnPoint.Length)]);
                break;
            default:
                break;
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
