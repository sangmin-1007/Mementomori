using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class UI_Skill : UI_Base<UI_Skill>
{
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
