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

    [SerializeField] private TextMeshProUGUI timeText;

    private PlayerStatsHandler playerStats;

    private float time;

    private float curHp, maxHP;
    private float curStamina, maxStamina;
    private float curExp, maxExp;

    private void Start()
    {
        playerStats = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();

        maxHP = playerStats.CurrentStates.maxHealth;
        maxStamina = playerStats.CurrentStates.maxStamina;

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

        curHp = playerStats.CurrentStates.maxStamina;
        curStamina = playerStats.CurrentStates.maxStamina;

        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, 0, Time.deltaTime * 3f);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, 0, Time.deltaTime * 5f);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, 1f, Time.deltaTime * 1.5f);

        if(expBar.fillAmount >= 0.99f)
        {
            expBar.fillAmount = 0f;
        }
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
}
