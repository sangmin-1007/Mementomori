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
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    [Range(1, 100)] public float maxStamina;

    //���� ������
    public AttackSO attackSO;

}
