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
            ran[0] = Random.Range(0, skills.Length);
            ran[1] = Random.Range(0, skills.Length);
            ran[2] = Random.Range(0, skills.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }
        for(int index = 0; index < ran.Length; index++)
        {
            Skill ranSkill = skills[ran[index]];
            if(ranSkill.skillLevel == ranSkill.data.damages.Length)
            {
                skills[Random.Range(0,3)].gameObject.SetActive(true);
            }
            else
            {
                ranSkill.gameObject.SetActive(true);
            }    
        }
    }

    [SerializeField] private List<PlayerStats> statsModifier;
    [SerializeField] int healValue = 10;
    private HealthSystem healthSystem;
    
    public void GetSkill()
    {
        Time.timeScale = 1;
        healthSystem = Managers.GameSceneManager.Player.GetComponent<HealthSystem>();
        healthSystem.ChangeHealth(healValue);
        PlayerStatsHandler statsHandler = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
        foreach (PlayerStats stat in statsModifier)
        {
            statsHandler.AddStatModifire(stat);
        }
        Managers.UI_Manager.HideUI<UI_Skill>();
    }
    private IEnumerator SkillDelay()
    {
        healthSystem = Managers.GameSceneManager.Player.GetComponent<HealthSystem>();
        healthSystem.ChangeHealth(healValue);
        PlayerStatsHandler statsHandler = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
        foreach (PlayerStats stat in statsModifier)
        {
            statsHandler.AddStatModifire(stat);
        }
        Managers.UI_Manager.HideUI<UI_Skill>();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        yield break;
    }

}
