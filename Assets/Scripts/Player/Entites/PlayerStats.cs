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
    [Range(0, 100)] public int maxHealth;
    [Range(0f, 20f)] public float speed;
    [Range(0, 100)] public float maxStamina;

    //공격 데이터
    public AttackSO attackSO;

}
