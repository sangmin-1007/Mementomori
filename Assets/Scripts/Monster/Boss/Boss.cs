using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    HealthSystem _healthSystem;
    PlayerStatsHandler stats;

    private UI_HUD _hud;
    private float maxHP, curHp;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        stats = GetComponent<PlayerStatsHandler>();

        _healthSystem.OnDeath += BossDie;
    }

    private void Start()
    {
        if(Managers.UI_Manager.IsActive<UI_HUD>())
        {
            _hud = Managers.UI_Manager.UI_List["UI_HUD"].GetComponent<UI_HUD>();
        }

        _hud.bossHpBar.gameObject.SetActive(true);

        maxHP = stats.baseStats.maxHealth;
    }

    private void Update()
    {
        curHp = _healthSystem.CurrentHealth;

        _hud.bossHpBar.hpBar.fillAmount = Mathf.Lerp(_hud.bossHpBar.hpBar.fillAmount, curHp / maxHP, Time.deltaTime * 3f);
    }

    private void BossDie()
    {
        _hud.bossHpBar.gameObject.SetActive(false);
        Spawner.boss = false;
        Spawner.stage++;
        if(Spawner.stage == 5)
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
