using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Experience/Level")]
    public int expriecne = 0;
    public int level = 1;
    public int expriecneCap;

    public List<LevelRange> levelRanges;

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
        if(Managers.UI_Manager.IsActive<UI_GameOver>())
        return;

        if (expriecne >= expriecneCap)
        {
            level++;
            if (!Managers.UI_Manager.IsActive<UI_Skill>())
            {
                Managers.UI_Manager.ShowUI<UI_Skill>().Next();
                HealthSystem healthSystem = Managers.GameSceneManager.Player.GetComponent<HealthSystem>();
                healthSystem._timeSinceLastChange = 0.51f;
                Time.timeScale = 0;
            }
            expriecne -= expriecneCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            expriecneCap += experienceCapIncrease;
        }
    }
}
