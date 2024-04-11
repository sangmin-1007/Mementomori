using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public enum CharacterType
{
    Monster,
    Player
}

public class PlayerStatsHandler : MonoBehaviour
{
    private const float MinAttackDelay = 0.03f;
    private const float MinAttackPower = 0.5f;
    private const float MinAttackSize = 0.4f;
    private const float MinAttackSpeed = .1f;

    private const float MinSpeed = 0.8f;
    private const int MinMaxHealth = 5;

    [SerializeField] private CharacterType characterType;

    [SerializeField] private PlayerStats baseStats;
    public int allHealth;
    public float allAttack;
    public float allDefense;
    public PlayerStats CurrentStates { get; private set; }
    public List<PlayerStats> statsModifiers = new List<PlayerStats>();

    private void Awake()
    {
        UpdatePlayerStats();
        if(characterType == CharacterType.Player)
        {
            EquipStatApply();
            Managers.PlayerEquipStatsManager.UpdateStats += EquipStatApply;
        }
        FixPlayerStat();
    }
    private void Update()
    {
        FixPlayerStat();
    }
    public void AddStatModifire(PlayerStats statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdatePlayerStats();
        EquipStatApply();
    }

    public void RemoveStatModifier(PlayerStats statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdatePlayerStats();
    }
    
    public void UpdatePlayerStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        CurrentStates = new PlayerStats { attackSO = attackSO };
        
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
        //LimitAllStats();
    }
    private void UpdateStats(Func<float, float, float> operation, PlayerStats newModofier)
    {
        CurrentStates.maxHealth = (int)operation(CurrentStates.maxHealth, newModofier.maxHealth);
        CurrentStates.speed = operation(CurrentStates.speed, newModofier.speed);
        CurrentStates.maxStamina = operation(CurrentStates.maxStamina, newModofier.maxStamina);
        CurrentStates.maxDefense = (int)operation(CurrentStates.maxDefense, newModofier.maxDefense);

        if (CurrentStates.attackSO == null || newModofier.attackSO == null)
            return;

        UpdateAttackStats(operation, CurrentStates.attackSO, newModofier.attackSO);

        if (CurrentStates.attackSO.GetType() != newModofier.attackSO.GetType())
        {
            return;
        }

        switch (CurrentStates.attackSO)
        {
            case AttackSO _:
                ApplyDeaaultStats(operation, newModofier);
                break;

        }
    }
    private void UpdateAttackStats(Func<float, float, float> operation, AttackSO currentAttack, AttackSO newAttack)
    {
        if (currentAttack == null || newAttack == null)
        {
            return;
        }

        currentAttack.delay = operation(currentAttack.delay, newAttack.delay);
        currentAttack.power = operation(currentAttack.power, newAttack.power);
        currentAttack.size = operation(currentAttack.size, newAttack.size);
        currentAttack.speed = operation(currentAttack.speed, newAttack.speed);
    }
    private void ApplyDeaaultStats(Func<float, float, float> opreation, PlayerStats newModifier)
    {
        AttackSO currentDefaultAttacks = (AttackSO)CurrentStates.attackSO;
    }

    //private void LimitStats(ref float stat, float minVal)
    //{
    //    stat = Mathf.Max(stat, minVal);
    //}
    //private void LimitAllStats()
    //{
    //    if(CurrentStates == null || CurrentStates.attackSO == null)
    //    {
    //        return;
    //    }

    //    LimitStats(ref CurrentStates.attackSO.delay, MinAttackDelay);
    //    LimitStats(ref CurrentStates.attackSO.power, MinAttackPower);
    //    LimitStats(ref CurrentStates.attackSO.size, MinAttackSize);
    //    LimitStats(ref CurrentStates.attackSO.speed, MinAttackSpeed);
    //    LimitStats(ref CurrentStates.speed, MinSpeed);
    //    CurrentStates.maxHealth = Mathf.Max(CurrentStates.maxHealth, MinMaxHealth);
    //}

    public void EquipStatApply()
    {
        CurrentStates.itemDefense = baseStats.itemDefense + Managers.PlayerEquipStatsManager.def;
        CurrentStates.speed = baseStats.speed + Managers.PlayerEquipStatsManager.speed;
        CurrentStates.attackSO.delay = Mathf.Max(baseStats.attackSO.delay - Managers.PlayerEquipStatsManager.atkSpeed, 0.1f);
        CurrentStates.itemAttack = baseStats.itemAttack + Managers.PlayerEquipStatsManager.damage;
        CurrentStates.itemHealth = baseStats.itemHealth + (int)Managers.PlayerEquipStatsManager.hp;
        CurrentStates.maxStamina = baseStats.maxStamina + Managers.PlayerEquipStatsManager.stamina;
    }
    public void FixPlayerStat()
    {
        if(characterType == CharacterType.Player)
        {
            allHealth = CurrentStates.maxHealth + CurrentStates.itemHealth;
            allAttack = CurrentStates.attackSO.power + CurrentStates.itemAttack;
            allDefense = CurrentStates.maxDefense + CurrentStates.itemDefense;
        }
    }
}
