using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level : MonoBehaviour
{
    //���� ����
    //���� ����
    public int startLevel;
    //�� ����
    public int endLevel;
    //����ġ��
    public int experienceCapIncrease;

    [Header("Experience/Level")]
    public int expriecne = 0;
    public int level = 1;
    public int expriecneCap;

    public List<Level> levelRanges;

    void Start()
    {
        expriecneCap = levelRanges[0].experienceCapIncrease;
    }

    public void IncreaseExprience(int amount)
    {
        expriecne += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if(expriecne >= expriecneCap)
        {
            level++;
            expriecne -= expriecneCap;

            int experienceCapIncrease = 0;
            foreach(Level range in levelRanges) 
            { 
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            expriecneCap += experienceCapIncrease;
        }
    }
}

