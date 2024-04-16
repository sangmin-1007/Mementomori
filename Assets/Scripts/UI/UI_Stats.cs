using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Stats : UI_Base<UI_Stats>
{
    [Header("бс Stats")]
    [SerializeField] private Text attackStatText;
    [SerializeField] private Text attackSpeedStatText;
    [SerializeField] private Text maxHpStatText;
    [SerializeField] private Text maxStanminaStatText;
    [SerializeField] private Text defStatText;
    [SerializeField] private Text moveSpeedText;

    [SerializeField] private Text deathCount;

    private PlayerStatsHandler playerStats;

    public override void OnEnable()
    {
        base.OnEnable();

       if(SceneManager.GetActiveScene().name == "LobbyScene")
       {
            playerStats = Managers.LobbySceneManager.Player.GetComponent<PlayerStatsHandler>();
       }
       else if(SceneManager.GetActiveScene().name == "GameScene")
       {
            playerStats = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
       }

        UpdateStatText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Managers.UI_Manager.IsActive<UI_Stats>())
            {
                CloseUI();
            }
        }
    }

    private void UpdateStatText()
    {
        deathCount.text = Managers.UserData.playerDeathCount.ToString();
        PlayerEquipStatsManager equipStat = Managers.PlayerEquipStatsManager;


        attackStatText.text = $"{playerStats.CurrentStates.attackSO.power}(+{equipStat.damage})";
        attackSpeedStatText.text = AttackSpeedResult(playerStats.CurrentStates.attackSO.delay) + "/s";
        maxHpStatText.text = $"{playerStats.CurrentStates.maxHealth}(+{equipStat.hp})";
        maxStanminaStatText.text = $"{playerStats.CurrentStates.maxStamina} (+{equipStat.stamina})";
        defStatText.text = $"{playerStats.CurrentStates.maxDefense}(+{equipStat.def})";
        moveSpeedText.text = $"{playerStats.CurrentStates.speed}(+{equipStat.speed})";

        deathCount.text = Managers.UserData.playerDeathCount.ToString();
    }

    private string AttackSpeedResult(float atkSpeed)
    {
        float myAttackSpeed = Mathf.Max(atkSpeed, 1f);
        return myAttackSpeed.ToString();
    }
}


