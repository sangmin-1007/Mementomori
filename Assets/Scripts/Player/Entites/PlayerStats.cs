using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override,
}

[Serializable]
public class PlayerStats
{
    public StatsChangeType statsChangeType;

    public int maxHealth;
    public int itemHealth;
    public float itemAttack;
    public float speed;
    public float maxStamina;
    public float maxDefense;
    public float itemDefense;

    public AttackSO attackSO;
    
}

