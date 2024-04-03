using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillData data;
    public int skillLevel;
    public Skill_1 skill_1;

    [SerializeField] private List<PlayerStats> statsModifier;
    private HealthSystem healthSystem;
    [SerializeField] int healValue = 10;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;


    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.skillIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.skillName;

    }

    void OnEnable()
    {
        textLevel.text = "Lv." + skillLevel;

        switch(data.skillType)
        {
            case SkillData.SkillType.Melee:
                textDesc.text = string.Format(data.skillDesc, data.damages[skillLevel]);
                break;
            case SkillData.SkillType.Range:
                textDesc.text = string.Format(data.skillDesc, data.damages[skillLevel]);
                break;
            case SkillData.SkillType.Statup:
                textDesc.text = string.Format(data.skillDesc, data.damages[skillLevel]);
                break;
            case SkillData.SkillType.Addskill:
                textDesc.text = string.Format(data.skillDesc, data.damages[skillLevel]);
                break;
            case SkillData.SkillType.Heal:
                textDesc.text = string.Format(data.skillDesc);
                break;
        }
        
    }

    public void OnClick()
    {
        switch (data.skillType)
        {
            case SkillData.SkillType.Melee:
            case SkillData.SkillType.Range:
                if(skillLevel == 0)
                {
                    GameObject newWeapon = new GameObject();
                    skill_1 = newWeapon.AddComponent<Skill_1>();
                    skill_1.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[skillLevel];
                    nextCount += data.counts[skillLevel];

                    skill_1.LevelUp(nextDamage,nextCount);
                }
                break;
            case SkillData.SkillType.Statup:
                if(skillLevel == 0)
                {
                    healthSystem = Managers.GameSceneManager.Player.GetComponent<HealthSystem>();
                    healthSystem.ChangeHealth(healValue);
                    PlayerStatsHandler statsHandler = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
                    foreach (PlayerStats stat in statsModifier)
                    {
                        statsHandler.AddStatModifire(stat);
                    }
                }
                break;
            case SkillData.SkillType.Addskill:
                break;
        }
        skillLevel++;
        if(skillLevel == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
        Time.timeScale = 1;
        Managers.UI_Manager.HideUI<UI_Skill>();
    }
}
