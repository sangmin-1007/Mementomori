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
    [Range(0, 500)] public int maxHealth;
    [Range(0, 200)] public int itemHealth;
    public float itemAttack;
    [Range(0f, 20f)] public float speed;
    [Range(0, 100)] public float maxStamina;
    [Range(0, 50)] public float maxDefense;
    [Range(0, 50)] public float itemDefense;

    //공격 데이터
    public AttackSO attackSO;
    
}

