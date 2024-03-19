using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private PlayerStatsHandler _statsHandler;
    private float _timeSinceLastChange = float.MaxValue;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth {  get; private set; }
    public float MaxHealth => _statsHandler.CurrentStates.maxHealth;

    private void Awake()
    {
       _statsHandler = GetComponent<PlayerStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statsHandler.CurrentStates.maxHealth;

        OnDeath += Managers.GameManager.GameOver;

        currentStamina = _statsHandler.CurrentStates.maxStamina;
    }

    private void Update()
    {
        //마지막의 변경된 값이 < 0.5 초보다 작다면
        if(_timeSinceLastChange < healthChangeDelay)
        {
            _timeSinceLastChange += Time.deltaTime;
            if(_timeSinceLastChange <= healthChangeDelay )
            {
                OnInvincibilityEnd?.Invoke();
            }
        }
        SPRechargeTime();
        SPRecover();
    }

    public bool ChangeHealth(float change)
    {
        if(change == 0 || _timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        _timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        //피가 찰 경우
        if(change > 0)
        {
            OnHeal?.Invoke();
        }
        //피가 닳을 경우
        else
        {
            OnDamage?.Invoke();
        }
        //사망
        if(CurrentHealth <= 0f)
        {
            CallDeath();
        }
        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }

    public float currentStamina;

    [SerializeField] private float staminaRecoveryRate;
    private float rechargeTime;
    private float currentRechargeTime;
    private bool staminaUsed;

    public void DecreaseStamina(float _count)
    {
        staminaUsed = true;
        currentRechargeTime = 0;
        if (currentStamina - _count > 0)
        {
            currentStamina -= _count;
        }
        else
            currentStamina = 0;
    }
    private void SPRechargeTime()
    {
        if (staminaUsed)
        {
            if (currentRechargeTime < rechargeTime)
                currentRechargeTime++;
            else
                staminaUsed = false;
        }
    }
    private void SPRecover()
    {
        if (!staminaUsed && currentStamina < _statsHandler.CurrentStates.maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
        }
    }
    public float GetCurrentSP()
    {
        return currentStamina;
    }
}
