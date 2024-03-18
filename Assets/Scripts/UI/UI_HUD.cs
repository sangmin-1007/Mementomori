using System;
using System.Collections;
using System.Collections.Generic;
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


    private int curHp, maxHP;
    private int curStamina, maxStamina;
    private int curExp, maxExp;

    private void Start()
    {
        miniMapAddButton.onClick.AddListener(OnClickAddButton);
        miniMapSubButton.onClick.AddListener(OnClickSubButton);
    }

    private void OnDisable()
    {
        DestroyUI();
    }

    private void Update()
    {
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
}
