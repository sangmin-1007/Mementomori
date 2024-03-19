using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    [SerializeField] private PlayerStats baseStats;
    public PlayerStats CurrentStates { get; private set; }
    public List<PlayerStats> statsModifiers = new List<PlayerStats>();

    private void Awake()
    {
        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        CurrentStates = new PlayerStats { attackSO = attackSO };
        //TODO
        CurrentStates.statsChangeType = baseStats.statsChangeType;
        CurrentStates.maxHealth = baseStats.maxHealth;
        CurrentStates.speed = baseStats.speed;
        CurrentStates.maxStamina = baseStats.maxStamina;
    }
    private void Update()
    {
        SPRechargeTime();
        SPRecover();
    }
    private void Start()
    {
        currentStamina = CurrentStates.maxStamina;
    }

    private float currentStamina;

    [SerializeField] private float staminaRecoveryRate;
    [SerializeField] private float rechargeTime;
    [SerializeField] private float currentRechargeTime;
    private bool staminaUsed;

    public void DecreaseStamina(float _count)
    {
        staminaUsed = true;
        currentRechargeTime = 0;
        if(currentStamina - _count > 0)
        {
            currentStamina -= _count;
        }
        else
            currentStamina = 0;
    }
    private void SPRechargeTime()
    {
        if(staminaUsed)
        {
            if (currentRechargeTime < rechargeTime)
                currentRechargeTime++;
            else
                staminaUsed = false;
        }
    }
    private void SPRecover()
    {
        if(!staminaUsed && currentStamina < 100)
        {
            currentStamina += staminaRecoveryRate*Time.deltaTime;
        }
    }
    public float GetCurrentSP()
    {
        return currentStamina;
    }
}
