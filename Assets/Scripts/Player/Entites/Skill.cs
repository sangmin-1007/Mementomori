using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Skill : UI_Base<Skill>
{
    [SerializeField] private List<PlayerStats> statsModifier;
    [SerializeField] int healValue = 10;
    private HealthSystem healthSystem;

    public static Skill instance;

    private void Awake()
    {
        instance = this;
    }
    
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
        Managers.UI_Manager.HideUI<Skill>();
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
        Managers.UI_Manager.HideUI<Skill>();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        yield break;
    }
}
