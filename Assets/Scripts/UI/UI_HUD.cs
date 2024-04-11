using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : UI_Base<UI_HUD>
{
    [SerializeField] private CanvasGroup miniMapCanvasGroup;
    [SerializeField] private Button miniMapSubButton;
    [SerializeField] private Button miniMapAddButton;

    [SerializeField] private Image hpBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Image expBar;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI timeText;

    private HealthSystem playerStats;
    private Level playerLevel;

    private float time;

    private float curHp, maxHP;
    private float curStamina, maxStamina;
    private float curExp, maxExp;

    private void Start()
    {
        playerStats = Managers.GameSceneManager.Player.GetComponent<HealthSystem>();
        playerLevel = Managers.GameSceneManager.Player.GetComponent<Level>();



        miniMapAddButton.onClick.AddListener(OnClickAddButton);
        miniMapSubButton.onClick.AddListener(OnClickSubButton);
    }

    private void OnDisable()
    {
        Managers.GameManager.ResetTimer();
        DestroyUI();
    }

    private void Update()
    {
        Timer();
        UpdateHUD();
    }

    private float GetPercentage(float curValue, float maxValue)
    {
        return curValue/ maxValue;
    }

    private void OnClickAddButton()
    {
        if(miniMapCanvasGroup.alpha <= 1f)
        {
            miniMapCanvasGroup.alpha += 0.1f;
        }
    }

    private void OnClickSubButton()
    {
        if(miniMapCanvasGroup.alpha >= 0.2f)
        {
            miniMapCanvasGroup.alpha -= 0.1f;
        }
        
    }

    private void Timer()
    {
        time = Managers.GameManager.timer;

        Managers.GameManager.Ontimer();

        int min = Mathf.Max(0, (int)time / 60);
        int sec = Mathf.Max(0, (int)time % 60);

        timeText.text = min.ToString("D2") + ":" + sec.ToString("D2");
    }

    private void UpdateHUD()
    {
        maxHP = playerStats.MaxHealth;
        maxStamina = playerStats.MaxStamina;
        curHp = playerStats.CurrentHealth;
        curStamina = playerStats.CurrentStamina;
        maxExp = playerLevel.expriecneCap;
        curExp = playerLevel.expriecne;

        levelText.text = playerLevel.level.ToString();

        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, GetPercentage(curHp, maxHP), Time.deltaTime * 3f);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, GetPercentage(curStamina, maxStamina), Time.deltaTime * 5f);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, GetPercentage(curExp, maxExp), Time.deltaTime * 1.5f);

        if (expBar.fillAmount >= 0.99f)
        {
            expBar.fillAmount = 0f;
        }
    }
}
