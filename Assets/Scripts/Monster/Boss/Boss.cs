using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDeath += BossDie;
    }

    private void BossDie()
    {
        Spawner.boss = false;
        Spawner.stage++;
        if(Spawner.stage == 5)
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
