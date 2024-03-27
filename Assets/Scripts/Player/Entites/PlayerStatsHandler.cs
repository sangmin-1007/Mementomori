using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerStatsHandler : MonoBehaviour
{
    private const float MinAttackDelay = 0.03f;
    private const float MinAttackPower = 0.5f;
    private const float MinAttackSize = 0.4f;
    private const float MinAttackSpeed = .1f;

    private const float MinSpeed = 0.8f;
    private const int MinMaxHealth = 5;

    [SerializeField] private PlayerStats baseStats;
    public PlayerStats CurrentStates { get; private set; }
    public List<PlayerStats> statsModifiers = new List<PlayerStats>();

    private void Awake()
    {
        UpdatePlayerStats();
    }
    public void AddStatModifire(PlayerStats statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdatePlayerStats();
    }

    public void RemoveStatModifier(PlayerStats statModifier)
    {
        statsModifiers.Remove(statModifier);
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
        //CurrentStates.statsChangeType = baseStats.statsChangeType;
        //CurrentStates.maxHealth = baseStats.maxHealth;
        //CurrentStates.speed = baseStats.speed;
        //CurrentStates.maxStamina = baseStats.maxStamina;
        UpdateStats((a, b) => b, baseStats);
        if(CurrentStates.attackSO != null)
        {
            CurrentStates.attackSO.target = baseStats.attackSO.target;
        }

        foreach (PlayerStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            if(modifier.statsChangeType == StatsChangeType.Override)
            {
                UpdateStats((o, o1) => o1, modifier);
            }
            else if(modifier.statsChangeType == StatsChangeType.Add)
            {
                UpdateStats((o, o1) => o + o1, modifier);
            }
            else if(modifier.statsChangeType == StatsChangeType.Multiple)
            {
                UpdateStats((o, o1) => o * o1, modifier);
            }
        }
        LimitAllStats();
    }
    private void UpdateStats(Func<float, float, float> operation, PlayerStats newModofier)
    {
        CurrentStates.maxHealth = (int)operation(CurrentStates.maxHealth, newModofier.maxHealth);
        CurrentStates.speed = operation(CurrentStates.speed, newModofier.speed);
        CurrentStates.maxStamina = operation(CurrentStates.maxStamina, newModofier.maxStamina);

        //if(CurrentStates.attackSO == null || newModofier .attackSO == null)
        //    return;

        //UpdateAttackStats(operation,CurrentStates.attackSO,newModofier.attackSO);

        //if(CurrentStates.attackSO.GetType() != newModofier.attackSO.GetType())
        //{
        //    return;
        //}

        //switch (CurrentStates.attackSO)
        //{
        //    case DefaultAttackData _:
        //        ApplyDeaaultStats(operation, newModofier);
        //        break;

        //}
    }
    //private void UpdateAttackStats(Func<float,float,float> operation,AttackSO currentAttack,AttackSO newAttack)
    //{
    //    if(currentAttack == null || newAttack == null)
    //    {
    //        return;
    //    }

    //    currentAttack.delay = operation(currentAttack.delay, newAttack.delay);
    //    currentAttack.power = operation(currentAttack.power, newAttack.power);
    //    currentAttack.size = operation(currentAttack.size, newAttack.size);
    //    currentAttack.speed = operation(currentAttack.speed, newAttack.speed);
    //}
    //private void ApplyDeaaultStats(Func<float, float, float > opreation,PlayerStats newModifier)
    //{
    //    DefaultAttackData currentDefaultAttacks = (DefaultAttackData)CurrentStates.attackSO;
    //}

    private void LimitStats(ref float stat, float minVal)
    {
        stat = Mathf.Max(stat, minVal);
    }
    private void LimitAllStats()
    {
        if(CurrentStates == null || CurrentStates.attackSO == null)
        {
            return;
        }

        LimitStats(ref CurrentStates.attackSO.delay, MinAttackDelay);
        LimitStats(ref CurrentStates.attackSO.power, MinAttackPower);
        LimitStats(ref CurrentStates.attackSO.size, MinAttackSize);
        LimitStats(ref CurrentStates.attackSO.speed, MinAttackSpeed);
        LimitStats(ref CurrentStates.speed, MinSpeed);
        CurrentStates.maxHealth = Mathf.Max(CurrentStates.maxHealth, MinMaxHealth);
    }
}
