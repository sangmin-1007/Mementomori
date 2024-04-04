using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private PlayerStatsHandler _statsHandler;
    public float _timeSinceLastChange = float.MaxValue;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth {  get; private set; }
    public float MaxHealth => _statsHandler.CurrentStates.maxHealth;
    public float CurrentDefense { get; set; }

    private void Awake()
    {
       _statsHandler = GetComponent<PlayerStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statsHandler.CurrentStates.maxHealth;
        currentStamina = _statsHandler.CurrentStates.maxStamina;
        CurrentDefense = _statsHandler.CurrentStates.maxDefense;
    }

    private void Update()
    {
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


        if(change > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
        }
        if(CurrentHealth <= 0f)
        {
            CallDeath();
        }
        Debug.Log($"바뀐체력 : {change}, 현재체력 : {CurrentHealth} 최대체력 : {MaxHealth}");
        return true;
    }

    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }

    //스태미너 관련
    public float currentStamina { get; private set; }
    public float MaxStamina => _statsHandler.CurrentStates.maxStamina;

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
