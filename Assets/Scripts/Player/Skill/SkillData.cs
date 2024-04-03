using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill",menuName ="Scriptble Object/SkillData")]
public class SkillData : ScriptableObject
{
    public enum SkillType { Melee, Range, Statup, Addskill, Heal}

    [Header("Main Info")]
    public SkillType skillType;
    public int skillId;
    public string skillName;
    [TextArea]
    public string skillDesc;
    public Sprite skillIcon;

    [Header("Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("Weapon")]
    public GameObject projectile;
}
