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
        //경험치량에 도달
        if (expriecne >= expriecneCap)
        {
            level++;
            if (!Managers.UI_Manager.IsActive<UI_Skill>())
            {
                Managers.UI_Manager.ShowUI<UI_Skill>();
                Time.timeScale = 0;
                //StartCoroutine(StopTime());
            }
            //현재 경험치 초기화
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
    private IEnumerator StopTime()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        yield break;
    }
}
