using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.CinemachineCore;

public enum BossType
{
    Dragonewt,
    Germud,
    CarcassesCollector,
    Ifrit
}

public class Boss : MonoBehaviour
{
    [SerializeField] private BossType bossType;

    HealthSystem _healthSystem;
    PlayerStatsHandler stats;
    
    static public bool gameEnd = false;

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

        BossNameChange(bossType);
        maxHP = stats.baseStats.maxHealth;
        _hud.bossHpBar.gameObject.SetActive(true);

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
            gameEnd = true;
            _hud.FadeInBackGround();
            Invoke("GameEnd", 10f);
        }
    }

    void GameEnd()
    {
        SceneManager.LoadScene("EndingScene");
    }

    private void BossNameChange(BossType bossType)
    {
        switch(bossType)
        {
            case BossType.Dragonewt:
                _hud.bossHpBar.bossNameText.text = "드라고뉴트";
                break;
            case BossType.Germud:
                _hud.bossHpBar.bossNameText.text = "게르뮈드";
                break;
            case BossType.CarcassesCollector:
                _hud.bossHpBar.bossNameText.text = "시체 수집가";
                break;
            case BossType.Ifrit:
                _hud.bossHpBar.bossNameText.text = "악마 : 이프리트";
                break;
        }
    }
}
