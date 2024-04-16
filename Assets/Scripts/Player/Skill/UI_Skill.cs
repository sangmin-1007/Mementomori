using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class UI_Skill : UI_Base<UI_Skill>
{
    Skill[] skills;

    private void Awake()
    {
        skills = GetComponentsInChildren<Skill>(true);
    }
    public void Next()
    {
        foreach (Skill skill in skills)
        {
            skill.gameObject.SetActive(false);
        }
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, skills.Length - 2);
            ran[1] = Random.Range(0, skills.Length - 2);
            ran[2] = Random.Range(0, skills.Length - 2);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }
        for (int index = 0; index < ran.Length; index++)
        {
            Skill ranSkill = skills[ran[index]];
            if (ranSkill.skillLevel == ranSkill.data.damages.Length && ranSkill.data.skillId == 4)
            {
                skills[6].gameObject.SetActive(true);
            }
            else if (ranSkill.skillLevel == ranSkill.data.damages.Length && ranSkill.data.skillId == 5)
            {
                skills[7].gameObject.SetActive(true);
            }
            else
            {
                ranSkill.gameObject.SetActive(true);
            }
        }
    }
}
